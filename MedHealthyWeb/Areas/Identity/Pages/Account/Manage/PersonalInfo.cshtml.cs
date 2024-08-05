// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MedHealth.DataAccess.Repository.IRepository;
using MedHealthy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace MedHealthyWeb.Areas.Identity.Pages.Account.Manage
{
    public class PersonalInfoModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalInfoModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment hostEnviroment,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _hostEnviroment = hostEnviroment;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public string Gender { get; set; }
            public DateTime? BirthDay { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public string Allergies { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            ApplicationUser appuser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == user.Id, includeProperties: "Patient");
            
            Input = new InputModel
            {
                Gender = appuser.Patient.Gender,
                BirthDay = appuser.Patient.BirthDay,
                Country = appuser.Patient.Country,
                State = appuser.Patient.State,
                City = appuser.Patient.City,
                Street = appuser.Patient.Street,
                Allergies = appuser.Patient.Allergies,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            ApplicationUser appuser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == user.Id, includeProperties: "Patient");

            //await _userManager.SetUserNameAsync(user, Input.Name);
            appuser.Patient.Gender = Input.Gender;
            appuser.Patient.BirthDay = Input.BirthDay;
            appuser.Patient.Country = Input.Country;
            appuser.Patient.State = Input.State;
            appuser.Patient.City = Input.City;
            appuser.Patient.Street = Input.Street;
            appuser.Patient.Allergies = Input.Allergies;

            //_unitOfWork.Patient.Update(appuser.Patient);
            _unitOfWork.Save();
            await _userManager.UpdateAsync(appuser);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

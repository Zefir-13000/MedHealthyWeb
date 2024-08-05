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
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace MedHealthyWeb.Areas.Identity.Pages.Account.Manage
{
    public class PersonalInfoDoctorModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _hostEnviroment;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalInfoDoctorModel(
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
            public string Speciality { get; set; }
            [ValidateNever]
            public IEnumerable<SelectListItem> SpecialityList { get; set; }
            public float Price { get; set; }
            public string Address { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            ApplicationUser appuser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == user.Id, includeProperties: "Doctor");

            Input = new InputModel
            {
                SpecialityList = _unitOfWork.Speciality.GetAll().Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                Speciality = appuser.Doctor.Speciality?.Name ?? "",
                Price = appuser.Doctor.Price ?? 0,
                Address = appuser.Address
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


            ApplicationUser appuser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == user.Id, includeProperties: "Doctor");

            //await _userManager.SetUserNameAsync(user, Input.Name);
            if (Input.Speciality != null)
                appuser.Doctor.SpecialityId = _unitOfWork.Speciality.GetFirstOrDefault(u => u.Name == Input.Speciality).Id;
            appuser.Doctor.Price = Input.Price;
            appuser.Address = Input.Address;

            _unitOfWork.Doctor.Update(appuser.Doctor);
            _unitOfWork.Save();
            await _userManager.UpdateAsync(appuser);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

using MedHealth.DataAccess.Repository.IRepository;
using MedHealthy.Models;
using MedHealthy.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedHealthyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class AdminController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
		{
			var Specialities = _unitOfWork.Speciality.GetAll();
			return View(Specialities);
		}

        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speciality obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Speciality.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Speciality created succsessfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var SpecialityFromDb = _unitOfWork.Speciality.GetFirstOrDefault(u => u.Id == id);
			if (SpecialityFromDb == null)
			{
				return NotFound();
			}

			return View(SpecialityFromDb);
		}

		// POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Speciality obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Speciality.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Speciality updated succsessfully!";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var SpecialityFromDb = _unitOfWork.Speciality.GetFirstOrDefault(u => u.Id == id);
			if (SpecialityFromDb == null)
			{
				return NotFound();
			}
			return View(SpecialityFromDb);
		}

		// POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _unitOfWork.Speciality.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}

			_unitOfWork.Speciality.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Speciality deleted succsessfully!";
			return RedirectToAction("Index");
		}
	}
}

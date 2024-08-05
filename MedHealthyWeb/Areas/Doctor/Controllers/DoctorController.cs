using MedHealth.DataAccess.Repository.IRepository;
using MedHealth.Utility;
using MedHealthy.Models;
using MedHealthy.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedHealthyWeb.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public DoctorVM DoctorVM { get; set; }
        [BindProperty]
        public AppointmentVM AppointmentVM { get; set; }

        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId, includeProperties: "Doctor");
            DoctorVM = new()
            {
                Doctor = user.Doctor
            };
            return View(DoctorVM);
        }

        public IActionResult Appointment(int id)
        {
            Appointment appointment = _unitOfWork.Appointment.GetFirstOrDefault(u => u.Id == id, includeProperties: "Doctor,Patient");
            var PatientUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.PatientId == appointment.PatientId, includeProperties: "Patient");
            var DoctorUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.DoctorId == appointment.DoctorId, includeProperties: "Doctor");
            AppointmentVM = new()
            {
                Appointment = appointment,
                Patient = PatientUser,
                Doctor = DoctorUser
            };

            return View(AppointmentVM);
        }

        [HttpGet]
        public IActionResult GetAllAppointments()
        {
            IEnumerable<Appointment> Appointments = Enumerable.Empty<Appointment>();

            if (User.IsInRole(SD.Role_User_Doctor) || User.IsInRole(SD.Role_User_Admin))
            {
                Appointments = _unitOfWork.Appointment.GetAll();
            }

            return Json(new { data = Appointments });
        }
    }
}

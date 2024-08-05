using MedHealth.DataAccess.Repository.IRepository;
using MedHealthy.Models;
using MedHealthy.Models.ViewModels;
using MedHealthyWeb.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MedHealthyWeb.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public PatientVM PatientVM { get; set; }

        public BookAppointmentVM BookAppointmentVM { get; set; }

        public PatientController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // Medical Card
        public IActionResult Index()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId, includeProperties: "Patient");
            PatientVM = new()
            {
                Patient = user.Patient
            };
            return View(PatientVM);
        }

        public IActionResult Search(string text)
        {
            return RedirectToAction("BookAppointment", "Patient", new { area = "Patient", speciality = text });
        }

        public IActionResult BookAppointment([FromQuery(Name = "speciality")] string speciality)
        {

            IEnumerable<ApplicationUser> doctors;
            if (speciality.IsNullOrEmpty())
                doctors = _unitOfWork.ApplicationUser.GetAll(u => u.DoctorId != null, includeProperties: "Doctor,Doctor.Speciality");
            else
            {
                doctors = _unitOfWork.ApplicationUser.GetAll(u => u.DoctorId != null, includeProperties: "Doctor,Doctor.Speciality")
                                                     .Where(u => u.Doctor.Speciality.Name.Contains(speciality) || u.Name.Contains(speciality));
                ViewData["speciality"] = speciality;
            }
            BookAppointmentVM = new()
            {
                Doctors = doctors
            };

            return View(BookAppointmentVM);
        }

        public IActionResult BookAppointmentDetails(int doctorId)
        {
            var doctor = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.DoctorId == doctorId, includeProperties: "Doctor,Doctor.Speciality");

			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId, includeProperties: "Patient");

            AppointmentVM appointmentVM = new()
            {
                DoctorId = doctor.Id,
                Doctor = doctor,
                PatientId = user.Id,
                Patient = user,
                TimeList = GetTimeForAppointment(DateOnly.FromDateTime(DateTime.Now), doctorId)
            };

            return View(appointmentVM);
        }

        private List<TimeAppointment> GetTimeForAppointment(DateOnly date, int doctorId)
        {
            var result = _unitOfWork.Appointment.GetAll();

            List<TimeAppointment> timeAppointments = new List<TimeAppointment>();
            for (int i = 10; i < 19; i++)
            {
                bool enable = true;
                foreach (var item in result)
                {
                    if (item.DoctorId == doctorId && date == DateOnly.FromDateTime(item.DateStart) && TimeOnly.FromDateTime(item.DateStart).Hour == i)
                    {
                        enable = false; break;
                    }
                }
                timeAppointments.Add(new TimeAppointment(new TimeOnly(i, 0, 0), enable));
            }

            return timeAppointments;
        }

        [HttpGet]
        public IActionResult GetTimeForAppointmentAction(DateOnly date, int doctorId)
        {
            List<string> times = new List<string>();
            foreach (var item in GetTimeForAppointment(date, doctorId))
            {
                if (item.enable)
                {
                    times.Add(item.time.ToString());
                }
            }
            return Json(new { data = times });
        }

        [ActionName("BookAppointmentDetails")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookAppointmentDetails_POST(AppointmentVM obj)
        {
            if (ModelState.IsValid)
            {
                DateTime appointmentDate = obj.DateOnlyStart.ToDateTime(obj.TimeOnlyStart);
                Appointment appointment = new()
                {
                    DoctorId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Doctor.Id == Int32.Parse(obj.DoctorId), includeProperties: "Doctor").DoctorId,
                    PatientId = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == obj.PatientId).PatientId,
                    DateStart = appointmentDate,
                    DateEnd = appointmentDate + new TimeOnly(1,0,0).ToTimeSpan(),
				};

                _unitOfWork.Appointment.Add(appointment);
                _unitOfWork.Save();

                TempData["Success"] = "Appointment Created Successfully.";
                return RedirectToAction("BookAppointment", "Patient", new { area = "Patient" });
			}
            return View(obj);
        }
    }
}

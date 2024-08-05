using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models.ViewModels
{
    public struct TimeAppointment
    {
        public TimeOnly time;
		public bool enable;

        public TimeAppointment(TimeOnly time, bool enable)
        {
            this.time = time;
            this.enable = enable;
        }
    };
    public class AppointmentVM
    {
        public DateOnly DateOnlyStart { get; set; }
        public TimeOnly TimeOnlyStart { get; set; }
		[ValidateNever]
		public Appointment? Appointment { get; set; }
        public string? PatientId { get; set; }
        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual ApplicationUser? Patient { get; set; }

        public string? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual ApplicationUser? Doctor { get; set; }

        [ValidateNever]
        public List<TimeAppointment>? TimeList { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient? Patient { get; set; }

        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor? Doctor { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}

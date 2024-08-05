using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Details { get; set; }
        public float? Price { get; set; }
        public int? SpecialityId { get; set; }
        [ForeignKey("SpecialityId")]
        [ValidateNever]
        public virtual Speciality? Speciality { get; set; }
    }
}

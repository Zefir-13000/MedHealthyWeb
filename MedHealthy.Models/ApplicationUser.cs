﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string Address { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient? Patient { get; set; }

        public int? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor? Doctor { get; set; }
    }
}

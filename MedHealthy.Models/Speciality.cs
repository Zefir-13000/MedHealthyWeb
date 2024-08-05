using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models
{
    public class Speciality
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}

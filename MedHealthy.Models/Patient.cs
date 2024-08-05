using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Allergies { get; set; }
    }
}

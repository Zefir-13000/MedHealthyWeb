using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealthy.Models.ViewModels
{
    public class BookAppointmentVM
    {
        public IEnumerable<ApplicationUser> Doctors { get; set; }
    }
}

using MedHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository.IRepository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        void Add(Appointment appointment);
        public void Update(Appointment obj);
    }
}

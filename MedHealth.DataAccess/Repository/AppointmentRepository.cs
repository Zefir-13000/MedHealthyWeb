using MedHealth.DataAccess.Data;
using MedHealth.DataAccess.Repository.IRepository;
using MedHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void IAppointmentRepository.Update(Appointment obj)
        {
            _db.Appointments.Update(obj);
        }
    }
}

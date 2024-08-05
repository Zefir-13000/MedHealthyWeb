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
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        private ApplicationDbContext _db;
        public DoctorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void IDoctorRepository.Update(Doctor obj)
        {
            _db.Doctors.Update(obj);
        }
    }
}

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
    public class SpecialityRepository : Repository<Speciality>, ISpecialityRepository
    {
        private ApplicationDbContext _db;
        public SpecialityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Speciality obj)
        {
            _db.Specialities.Update(obj);
        }
    }
}

using MedHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository.IRepository
{
    public interface ISpecialityRepository : IRepository<Speciality>
    {
        void Update(Speciality obj);
    }
}

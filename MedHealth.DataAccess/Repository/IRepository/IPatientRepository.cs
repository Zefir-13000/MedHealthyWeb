using MedHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository.IRepository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        void Add(Patient patient);
        public void Update(Patient obj);
    }
}

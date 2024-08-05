using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISpecialityRepository Speciality { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IPatientRepository Patient { get; }
        IDoctorRepository Doctor { get; }
        IAppointmentRepository Appointment { get; }
        void Save();
    }
}

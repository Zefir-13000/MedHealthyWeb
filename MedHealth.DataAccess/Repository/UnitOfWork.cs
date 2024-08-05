using MedHealth.DataAccess.Data;
using MedHealth.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Speciality = new SpecialityRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Patient = new PatientRepository(_db);
            Doctor = new DoctorRepository(_db);
            Appointment = new AppointmentRepository(_db);
        }

        public ISpecialityRepository Speciality { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IPatientRepository Patient {  get; private set; }
        public IDoctorRepository Doctor { get; private set; }
        public IAppointmentRepository Appointment { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

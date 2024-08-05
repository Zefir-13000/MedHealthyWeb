﻿using MedHealth.DataAccess.Data;
using MedHealth.DataAccess.Repository.IRepository;
using MedHealthy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedHealth.DataAccess.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        void IPatientRepository.Update(Patient obj)
        {
            _db.Patients.Update(obj);
        }
    }
}

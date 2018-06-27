using System;
using System.Linq;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class PatientDao:CommonDao
    {    
        // Find a patient record based on userid
        public TM_Patient FindByUserId(int userId)
        {
            return db.TM_Patient.Where(p => p.UserId == userId).FirstOrDefault();
        }
        // Insert new patient and return an Id
        public int Insert(TM_Patient patient)
        {
            db.TM_Patient.Add(patient);
            db.SaveChanges();
            return patient.Id;
        }
        // Update a patient and return bool
        public bool Update(TM_Patient entity)
        {
            try
            {
                var patient = db.TM_Patient.Find(entity.Id);
                patient.AssuranceCard = entity.AssuranceCard;
                patient.IdentityCard = entity.IdentityCard;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }

        // Delete a patient and return bool
        public bool Delete(int Id)
        {
            try
            {
                var patient = db.TM_Patient.Find(Id);
                db.TM_Patient.Remove(patient);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}

using System;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class PatientDao:CommonDao
    {    
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
                /*
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }*/
                patient.AssuranceCard = entity.AssuranceCard;
                //patient.Status = entity.Status;
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

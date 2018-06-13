using System;
using Tm.Data.Models;

namespace Tm.Data.Functions
{

    public class DoctorDao:CommonDao
    {        
        public int Insert(TM_Doctor doctor)
        {
            db.TM_Doctor.Add(doctor);
            db.SaveChanges();
            return doctor.Id;
        }

        public bool Update(TM_Doctor entity)
        {
            try
            {
                var doctor  = db.TM_Doctor.Find(entity.Id);
                /*
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }*/
                //doctor.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //logging
                return false;
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var doctor = db.TM_Doctor.Find(Id);
                db.TM_Doctor.Remove(doctor);
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

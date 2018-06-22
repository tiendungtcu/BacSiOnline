using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels;
namespace Tm.Data.Functions
{

    public class DoctorDao:CommonDao
    {      
        // Diagnos an order
        public bool DiagnosOrder(int doctorId,long orderId,string conclusion)
        {
            var order = db.TM_DoctorOrder.Find(doctorId,orderId);
            if (order!=null)
            {               
                order.DiagnosisDate = DateTime.Now;
                order.Diagnosis = conclusion;
                order.Status = true;
                db.SaveChanges();
                return true;

            }
            return false;
        }

        // Find doctor who had diagnosed for a patient given by patienId
        // if not, return firt doctor
        public int FindDoctorForPatient(int patientId)
        {
            var records = db.TM_DoctorOrder.Where(d => d.PatientId == patientId).FirstOrDefault();
            if (records!=null)
            {
                return records.DoctorId;
            }
            else
            {
                var user = from u in db.TM_Users
                            from ur in u.TM_Roles
                            join r in db.TM_Roles on ur.Id equals r.Id
                            where r.Name.Equals("DOCTOR_GROUP")
                            select new
                            {
                               u.Id,
                               Name = u.UserName,
                               Role = r.Name,
                            };
                return user.FirstOrDefault().Id;

            }
        }
        
        // Insert new doctor
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

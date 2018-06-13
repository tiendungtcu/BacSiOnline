using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class OrderDao:CommonDao
    {
        public long Create(TM_Order entity)
        {
            db.TM_Order.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public long Create(int userId, string symptom)
        {
            TM_Order entity = new TM_Order();
            entity.OrderDate = DateTime.Now;
            entity.PatientId = userId;
            entity.Notes = symptom;
            db.TM_Order.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool AssignDoctorToOrder(int userId,long orderId)
        {
            try
            {
                TM_DoctorOrder entity = new TM_DoctorOrder();
                entity.DoctorId = userId;
                entity.OrderId = orderId;
                entity.Status = false;
                db.TM_DoctorOrder.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
            
        }
        public bool AssignDoctorToOrder(TM_DoctorOrder entity)
        {
            try
            {
                db.TM_DoctorOrder.Add(entity);
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

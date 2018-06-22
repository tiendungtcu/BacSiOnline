using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class OrderDao:CommonDao
    {
        public IEnumerable<OrderListModel> ListOrdersByPatient(int patientId)
        {
            /*
            return db.TM_Order.Join( // first order table
                db.TM_DoctorOrder,   // doctororder table
                order=>order.Id,    //on order.Id
                doctor=>doctor.OrderId, // equal doctor.OrderId
                (order,doctor)=>new OrderListModel // Select
                {
                    Id=order.Id,
                    Title =order.Title,
                    CreatedDate = order.OrderDate,
                    Doctor = doctor.TM_Users1.FullName,
                    Status = doctor.Status==true?"Đã chẩn đoán":"Chưa chẩn đoán",
                    DiagnosisNote = doctor.Diagnosis
                })
                .Where(joined=>joined.Id)
                */
               return  db.TM_Users.Where(user => user.Id == patientId)
                    .SelectMany(doctor => doctor.TM_DoctorOrder)
                    .Select(od => od.TM_Order)
                    .GroupBy(order => new { order.Id, order.Title, order.OrderDate, order.Notes,order.D })
                    .Select(g => new OrderListModel()
                    {
                        Id = g.Key.Id,
                        Title = g.Key.Title,
                        CreatedDate = g.Key.OrderDate,
                        Doctor = g.Key.
                        
                        g.Key.Name,
                        g.Key.CreatedBy,
                        g.Key.ModifiedOn,
                        TotalDrawDates = g.Count()
                    });
        }
        // Add Symptom to Order by orderId and symptomId
        public bool AddSymptom(long orderId,int symId)
        {
            try
            {
                TM_OrderSymptom entity = new TM_OrderSymptom();
                entity.OrderId = orderId;
                entity.SymptomId = symId;
                db.TM_OrderSymptom.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }                             
        }

        // Add Param to Order by orderId and paramId
        public bool AddParamById(long orderId, short paramId,float value, DateTime measuredDate)
        {
            try
            {
                TM_OrderParam entity = new TM_OrderParam();
                entity.OrderId = orderId;
                entity.ParamId = paramId;
                entity.Value = value;
                entity.MeasureDate = measuredDate;
                db.TM_OrderParam.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        // Add Param to Order by param CodeName
        public bool AddParamByName(long orderId,string codeName, float value,DateTime measuredDate)
        {
            try
            {
                short paramId = db.TM_MeasureParam.Where(d => d.CodeName.Equals(codeName)).FirstOrDefault().Id;
                return AddParamById(orderId, paramId,value,measuredDate);
            }
            catch (Exception)
            {

                return false;
            }
        }
        // Create new Order
        public long Create(TM_Order entity)
        {
            db.TM_Order.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }

        //Create new Order with userId and symptom
        public long Create(int userId, string symptom)
        {
            TM_Order entity = new TM_Order();
            entity.OrderDate = DateTime.Now;
            entity.PatientId = userId;
            entity.Notes = symptom;
            entity.Title = symptom.Length <= 20 ? symptom : symptom.Substring(0, 20); ;
            db.TM_Order.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool AssignDoctorToOrder(int doctorId,long orderId,int patienId)
        {
            try
            {
                TM_DoctorOrder entity = new TM_DoctorOrder();
                entity.DoctorId = doctorId;
                entity.OrderId = orderId;
                entity.Status = false;
                entity.PatientId = patienId;
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

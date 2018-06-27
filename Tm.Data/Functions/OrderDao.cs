using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Common;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using Tm.Data.ViewModels.Patient;

namespace Tm.Data.Functions
{
    public class OrderDao:CommonDao
    {
        // Return order Detail
        public OrderDetail GetDetail(long orderId,int patientId)
        {
            try
            {
                var order = db.TM_Order.Find(orderId); // get order
                if (order==null)
                {
                    return null;
                }
                var user = db.TM_Users.Find(patientId); // get user    
                if (user==null)
                {
                    return null;
                }            
                var doctororder = db.TM_DoctorOrder.Where(d => d.OrderId == orderId).FirstOrDefault();
                var doctor = db.TM_Users.Find(doctororder.DoctorId); // get doctor
                                                                     
                var model = new OrderDetail()
                {
                    Orders = GetOrderIdTitle(patientId),
                    SelectedOrder = orderId,
                    Age = user.DateOfBirth == null ? 0 : DateTime.Now.Year - user.DateOfBirth.Value.Year,
                    Gender = user.Gender == "M" ? "Nam" : user.Gender == "F" ? "Nữ" : "Khác",
                    PatientName = user.FullName,
                    ClinicalParams = GetOrderParams(orderId, Convert.ToInt16(AppConstants.CliParam)),
                    ParaclinicalParams = GetOrderParams(orderId, Convert.ToInt16(AppConstants.ParParam)),
                    DoctorAvatar = doctor.Avatar,
                    Title = order.Title,
                    Id = order.Id,
                    CreatedDate = order.OrderDate,
                    DoctorName = doctor.FullName,
                    Notes = order.Notes,
                    Symptoms = GetSymptoms(orderId),
                    DiagnosisNote = doctororder.Diagnosis,
                    DiagnosisDate = doctororder.DiagnosisDate,
                };
                return model;
            }
            catch (Exception)
            {

                return null;
            }
            
        }
        // Get symptoms of an order
        public IList<SymptomDetail> GetSymptoms(long orderId)
        {
            return db.TM_OrderSymptom.Where(od => od.OrderId == orderId)
                    .Join( //firt - orderparam table
                    db.TM_Symptom, // second MeasureParam
                    od => od.SymptomId, // on param.Id
                    symptom => symptom.Id, // equal oderparam.ParamId
                    (od, symptom) => new SymptomDetail //Select
                    {
                        Id = symptom.Id,
                        Name = symptom.Name,                       
                    }
                ).ToList();
        }
        // Get all Order consists of OrderId and OrderTitle by patientId
        public IList<OrderIdTitle> GetOrderIdTitle(int patientId)
        {
            return db.TM_Order.Where(o => o.PatientId == patientId && o.Status==true)
                              .Select(u => new OrderIdTitle
                              {
                                  Id = u.Id,
                                  Title = u.Title
                              }).ToList();
        }
        // Get all Params of an order with type
        public IList<ParamDetail> GetOrderParams(long orderId, int typeId)
        {
            return  db.TM_OrderParam.Where(od => od.OrderId == orderId)
                    .Join( //firt - orderparam table
                    db.TM_MeasureParam.Where(m=>m.Type==typeId) , // second MeasureParam
                    od => od.ParamId, // on param.Id
                    param => param.Id, // equal oderparam.ParamId
                    (od, param) => new ParamDetail //Select
                    {
                        Id = param.Id,
                        CodeName = param.CodeName,
                        MeasuredDate = od.MeasureDate,
                        Description = param.Description,
                        Unit = param.Unit,
                        Value = od.Value
                    }
                ).ToList();
        }

        // Get all Orders by PatienId
        public IEnumerable<OrderDetail> GetHistories(int patientId)
        {
            try
            {
                // Get all orderId
                var orderIds = db.TM_Order.Where(o => o.PatientId == patientId)
                                    .OrderByDescending(o => o.OrderDate)
                                    .Select(u => u.Id)
                                    .ToList();
                IList<OrderDetail> orderList = new List<OrderDetail>();
                foreach (var item in orderIds)
                {
                    orderList.Add(GetDetail(item, patientId));
                }
                return orderList;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        // Get all orders by doctor Id
        public IEnumerable<OrderListModel> ListOrdersByDoctor(int doctorId)
        {

            return db.TM_Order.Join( // first order table
                db.TM_DoctorOrder.Where(dr=>dr.DoctorId==doctorId),   // doctororder table
                order => order.Id,    //on order.Id
                doctor => doctor.OrderId, // equal doctor.OrderId
                (order, doctor) => new OrderListModel // Select
                {
                    Id = order.Id,
                    Title = order.Title,
                    CreatedDate = order.OrderDate,
                    Doctor = doctor.TM_Users.FullName,
                    Status = doctor.Status == true ? "Đã chẩn đoán" : "Chưa chẩn đoán",
                    DiagnosisNote = doctor.Diagnosis,
                    PatientId = order.PatientId
                });
                

        }

        // Get all Orders by PatienId
        public IEnumerable<OrderListModel> ListOrdersByPatient(int patientId)
        {

            return db.TM_Order.Join( // first order table
                db.TM_DoctorOrder,   // doctororder table
                order => order.Id,    //on order.Id
                doctor => doctor.OrderId, // equal doctor.OrderId
                (order, doctor) => new OrderListModel // Select
                {
                    Id = order.Id,
                    Title = order.Title,
                    CreatedDate = order.OrderDate,
                    Doctor = doctor.TM_Users.FullName,
                    Status = doctor.Status == true ? "Đã chẩn đoán" : "Chưa chẩn đoán",
                    DiagnosisNote = doctor.Diagnosis,
                    PatientId = order.PatientId
                })
                .Where(joined => joined.PatientId == patientId);

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

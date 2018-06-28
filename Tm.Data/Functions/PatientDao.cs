using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using Tm.Data.ViewModels.Doctor;

namespace Tm.Data.Functions
{
    public class PatientDao:CommonDao
    {
        
        // Get all patients by doctorid
        public IEnumerable<DoctorPatientModel> GetPatientsDetail(int doctorId)
        {
            var patientIds = GetDoctorPatientsId(doctorId);
            IList<DoctorPatientModel> patientList = new List<DoctorPatientModel>();
            foreach (var item in patientIds)
            {
                if (item>0)
                {
                    var patientDetail = GetDoctorPatientDetail(item);
                    if (patientDetail!=null)
                    {
                        patientList.Add(patientDetail);
                    }
                   
                }
               
            }
            return patientList;

        }

        // Get Id of patents for a doctor
        public IList<int> GetDoctorPatientsId(int doctorId)
        {
            // Get list of all patient's Ids and fullname for doctor
            var model = db.TM_DoctorOrder.Where(dr => dr.DoctorId == doctorId)
                                        .Join(
                                        db.TM_Users, //with table
                                        dr => dr.PatientId, //first key
                                        o => o.Id,    // second key
                                        (dr, o) => new { o.Id }
                                        ).Distinct();
            return model.Select(u=>u.Id).ToList();
        }

        // Get (DoctorPatientModel) patient detail for doctor
        public DoctorPatientModel GetDoctorPatientDetail(int patientId)
        {
                var model = new DoctorPatientModel();
                var patient = db.TM_Users.Find(patientId);
                var patDetail = db.TM_Patient.Where(p => p.UserId == patientId).FirstOrDefault();
                var address = db.UserAddresses.Where(ua => ua.UserId == patientId)
                                .Join(
                                 db.TM_Address, // first table
                                 u => u.AddressId, // first key
                                 a => a.Id,
                                 (u, a) => new { a.Id, a.Address, a.WardId }
                                 ).FirstOrDefault();

                model.PatientId = patientId;
                model.PatientName = patient.FullName;
                model.AssurenceCard = patDetail.AssuranceCard;
                model.Gender = patient.Gender == "M" ? "Nam" : patient.Gender == "F" ? "Nữ" : "Khác";
                model.LastOrder = GetLastOrder(patientId);
                model.Age = patient.DateOfBirth == null ? 0 : DateTime.Now.Year - patient.DateOfBirth.Value.Year;
                model.FullAddress = address==null?null:ToFullAddress((int)address.WardId, address.Address);                
                return model;
        }
        
        // Get lastorder time
        public DateTime GetLastOrder(int patientId)
        {
            return db.TM_Order.Where(o => o.PatientId == patientId)
                                    .OrderByDescending(o => o.OrderDate)
                                    .Select(o => o.OrderDate)
                                    .FirstOrDefault().Value;
        }

        // Get (PatientOrderDetail) Patient detail (patientId)
        public PatientOrderDetail GetPatientDetail(int patientId)
        {

                var model = new PatientOrderDetail();
                var patient = db.TM_Users.Find(patientId);
                var patDetail = db.TM_Patient.Where(p => p.UserId == patientId).FirstOrDefault();
                var address = db.UserAddresses.Where(ua => ua.UserId == patientId)
                                .Join(
                                 db.TM_Address, // first table
                                 u => u.AddressId, // first key
                                 a => a.Id,
                                 (u, a) => new { a.Id, a.Address, a.WardId }
                                 ).FirstOrDefault();

                model.PatienId = patientId;
                model.PatientName = patient.FullName;
                model.Avatar = patient.Avatar;
                model.PhoneNumber = patient.PhoneNumber;
                model.Gender = patient.Gender == "M" ? "Nam" : patient.Gender == "F" ? "Nữ" : "Khác";
                model.Email = patient.Email;
                model.LastLogin = patient.LastLogin;
                model.Age = patient.DateOfBirth == null ? 0 : DateTime.Now.Year - patient.DateOfBirth.Value.Year;
                model.Address = address==null?null:ToFullAddress((int)address.WardId, address.Address);
                model.AssurenceCard = patDetail.AssuranceCard;
                model.IdentityCard = patDetail.IdentityCard;
                model.Orders = ListOrdersByPatient(patientId);
                return model;
            
        }

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

        // Convert address to string 
        public string ToFullAddress(int wardId, string address)
        {
                var model = db.Wards.Where(w=>w.Id==wardId)
                    .Join(
                    db.Districts, // join with
                    ward => ward.DistrictID, // on first key
                    district => district.Id,// equals to second key
                    (ward, district) => new { WardName = ward.Name,WardType=ward.Type, DistrictName = district.Name, DistrictType=district.Type, district.ProvinceId } // selection
                    ).Join(
                    db.Provinces, // join with
                    first => first.ProvinceId, // first key
                    province => province.Id, // second key
                    (first, province) => new { first.WardName,first.WardType,first.DistrictType, first.DistrictName, province.Name,province.Type } //Selection
                    ).FirstOrDefault();
                if (model == null)
                {
                    return string.Empty;
                }
                StringBuilder result = new StringBuilder();
                result.Append(address); // dia chi
                result.Append(", ");
                result.Append(model.WardType+" ");
                result.Append(model.WardName); // xa
                result.Append(", ");
                result.Append(model.DistrictType + " ");
                result.Append(model.DistrictName); // Huyen
                result.Append(", ");
                result.Append(model.Type + " ");
                result.Append(model.Name); // Tinh
                return result.ToString();
            
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
    }
}

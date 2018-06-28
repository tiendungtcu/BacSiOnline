using System;
using System.Collections.Generic;
using System.Linq;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using Tm.Data.ViewModels.Doctor;
using Tm.Data.ViewModels.Patient;

namespace Tm.Data.Functions
{
    public class UserDao : CommonDao
    {

        public bool AddLastLogin(int userId)
        {
            try
            {
                var user = db.TM_Users.Find(userId);
                user.LastLogin = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        // Update Avatar 
        public bool UpdateAvatar(int userId,string avatar)
        {
            try
            {
                var user = db.TM_Users.Find(userId);
                user.Avatar = avatar;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        // Get list of patients by doctor Id
        public IEnumerable<int?> GetPatientsFromDoctor(int doctorId)
        {
            var model= db.TM_DoctorOrder.Where(dr => dr.DoctorId == doctorId)
                                    .Select(r => r.PatientId )
                                    //.GroupBy(dr => dr.Value)                                  
                                    .ToList();
            return model.Distinct();
        }

        // Get doctor profile detail
        public DoctorProfile GetDoctorProfile(int userId)
        {
            // Find patient account
            var user = db.TM_Users.Find(userId);
            if (user == null)
            {
                return null;
            }
            var doctor = db.TM_Doctor.Where(p => p.UserId == userId).FirstOrDefault();
            if (doctor == null)
            {
                doctor = new TM_Doctor() { Major = string.Empty, IdentityCard = string.Empty };
            }
            // Find 

            var model = new DoctorProfile();

            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Gender = user.Gender;
            model.Avatar = user.Avatar;
            model.DoB = user.DateOfBirth;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.Major = doctor.Major;
            model.IdentityCard = doctor.IdentityCard;
            model.Addresses = GetAddresses(userId);


            return model;

        }


        // Get patient profile detail
        public PatientProfile GetPatienProfile(int userId)
        {
            // Find patient account
            var user = db.TM_Users.Find(userId);
            if (user==null)
            {
                return null;
            }
            var patient = db.TM_Patient.Where(p => p.UserId == userId).FirstOrDefault();
            if (patient==null)
            {
                patient = new TM_Patient() { IdentityCard = string.Empty, AssuranceCard = string.Empty };
            }
            // Find 

            var model = new PatientProfile();

            model.Id = user.Id;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.Gender = user.Gender;
            model.Avatar = user.Avatar;
            model.DoB = user.DateOfBirth;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.AssuranceCard = patient.AssuranceCard;
            model.IdentityCard = patient.IdentityCard;
            model.Addresses = GetAddresses(userId);
                
            return model;
           
        }

        // Get addresses of a user
        public IList<AddressDetail> GetAddresses(int userId)
        {
            // Get list of address associate with user
            var addr = db.UserAddresses.Where(ua => ua.UserId == userId)
                                       .Select(u => u.Id)
                                       .ToList();
            IList<AddressDetail> addrList = new List<AddressDetail>();
            if (addr==null)
            {
                return null;
            }
            foreach (var item in addr)
            {
                addrList.Add(GetAddressDetail(item));
            }
            return addrList;
        }
        // Get address detail
        public AddressDetail GetAddressDetail(int Id)
        {
            var useraddr = db.UserAddresses.Find(Id);
            var addr = db.TM_Address.Find(useraddr.AddressId);

            var model = new AddressDetail();
            model.Id = Id;
            model.Address = addr.Address;
            model.StartDate = useraddr.StartDate;
            model.EndDate = useraddr.EndDate;
            model.WardId = addr.WardId;
            model.WardName = FindWardName((int)addr.WardId);
            model.DistrictName = FindDistrictName((int)addr.WardId);
            model.ProvinceName = FindProvinceName((int)addr.WardId);

            return model;
        }
        public string FindWardName(int wardId)
        {
            return db.Wards.Find(wardId).Name;
        }
        // Find District name
        public string FindDistrictName(int wardId)
        {
            return db.Wards.Where(a => a.Id == wardId)
                           .Join(db.Districts,
                           w => w.DistrictID,
                           d => d.Id,
                           (w, d) => new { d.Name, d.Id }
                           ).FirstOrDefault().Name;
        }

        // Find Province Name
        public string FindProvinceName(int wardId)
        {
            var res = db.Wards.Where(w => w.Id == wardId).
                    Join(db.Districts,
                        w => w.DistrictID,
                        d => d.Id,
                        (w, d) => new { d.Id, d.ProvinceId }
                        )
                    .Join(db.Provinces,
                        wd => wd.ProvinceId,
                        p => p.Id,
                        (wd, p) => new { p.Name }
                    )
                    .FirstOrDefault();
            return res.Name;
        }
    }
}

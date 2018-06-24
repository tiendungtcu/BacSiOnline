using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Tm.Data.Common;
using Tm.Data.Models;
using Tm.Data.ViewModels.Patient;

namespace Tm.Data.Functions
{
    public class UserDao : CommonDao
    {
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

            var model = new PatientProfile()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Gender = user.Gender,
                Avatar = user.Avatar,
                DoB = (DateTime)user.DateOfBirth,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AssuranceCard = patient.AssuranceCard,
                IdentityCard = patient.IdentityCard,
                Addresses = GetAddresses(userId)

            };
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

            var model = new AddressDetail()
            {

                Id = Id,
                Address = addr.Address,
                StartDate = useraddr.StartDate,
                EndDate = useraddr.EndDate,
                WardId = addr.WardId,
                WardName = FindWardName((int)addr.WardId),
                DistrictName = FindDistrictName((int)addr.WardId),
                ProvinceName = FindProvinceName((int)addr.WardId),
            };
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

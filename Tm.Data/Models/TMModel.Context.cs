﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tm.Data.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BACSIOIEntities : DbContext
    {
        public BACSIOIEntities()
            : base("name=BACSIOIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<TM_Address> TM_Address { get; set; }
        public virtual DbSet<TM_Doctor> TM_Doctor { get; set; }
        public virtual DbSet<TM_DoctorOrder> TM_DoctorOrder { get; set; }
        public virtual DbSet<TM_MeasureParam> TM_MeasureParam { get; set; }
        public virtual DbSet<TM_Notification> TM_Notification { get; set; }
        public virtual DbSet<TM_NotifyType> TM_NotifyType { get; set; }
        public virtual DbSet<TM_Order> TM_Order { get; set; }
        public virtual DbSet<TM_OrderParam> TM_OrderParam { get; set; }
        public virtual DbSet<TM_OrderSymptom> TM_OrderSymptom { get; set; }
        public virtual DbSet<TM_Patient> TM_Patient { get; set; }
        public virtual DbSet<TM_Roles> TM_Roles { get; set; }
        public virtual DbSet<TM_Symptom> TM_Symptom { get; set; }
        public virtual DbSet<TM_SysConfig> TM_SysConfig { get; set; }
        public virtual DbSet<TM_UserClaims> TM_UserClaims { get; set; }
        public virtual DbSet<TM_UserLogins> TM_UserLogins { get; set; }
        public virtual DbSet<TM_Users> TM_Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
    
        public virtual ObjectResult<ReceptionRegisterSp_Result> ReceptionRegisterSp(Nullable<int> userId, string address, Nullable<int> wardId, string symptom, Nullable<int> doctorId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var wardIdParameter = wardId.HasValue ?
                new ObjectParameter("WardId", wardId) :
                new ObjectParameter("WardId", typeof(int));
    
            var symptomParameter = symptom != null ?
                new ObjectParameter("Symptom", symptom) :
                new ObjectParameter("Symptom", typeof(string));
    
            var doctorIdParameter = doctorId.HasValue ?
                new ObjectParameter("DoctorId", doctorId) :
                new ObjectParameter("DoctorId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ReceptionRegisterSp_Result>("ReceptionRegisterSp", userIdParameter, addressParameter, wardIdParameter, symptomParameter, doctorIdParameter);
        }
    }
}

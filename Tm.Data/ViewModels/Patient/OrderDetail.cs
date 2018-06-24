using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tm.Data.Models;

namespace Tm.Data.ViewModels.Patient
{
    public class OrderDetail
    {
        [Key]
        public long Id { get; set; }
        public long SelectedOrder { get; set; }
        public IList<OrderIdTitle> Orders { get; set; }
        public string Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public IList<SymptomDetail> Symptoms { get; set; }
        public string Notes { get; set; }
        public IList<ParamDetail> ClinicalParams { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string BloodPressure { get; set; }
        public double? HeartBeat { get; set;
        }
        public IList<ParamDetail> ParaclinicalParams { get; set; }
        public string DoctorAvatar { get; set; }
        public string DoctorName { get; set; }
        public string DiagnosisNote { get; set; }
        public DateTime? DiagnosisDate { get; set; }

    }
}

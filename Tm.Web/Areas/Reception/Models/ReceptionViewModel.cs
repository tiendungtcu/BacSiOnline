using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Reception.Models
{
    public class ReceptionViewModel
    {

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }
        [Required]
        [Display(Name ="Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Năm sinh")]
        public int BirthYear { get; set; }
        [Required]
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Symptom { get; set; }
        public int DoctorId { get; set; }
    }
}
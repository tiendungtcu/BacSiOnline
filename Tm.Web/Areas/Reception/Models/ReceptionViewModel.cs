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
        [Display(Name = "Tỉnh")]
        public int ProvinceId { get; set; }
        [Display(Name = "Huyện")]
        public int DistrictId { get; set; }
        [Display(Name = "Xã")]
        public int WardId { get; set; }
        [Display(Name = "Triệu chứng ban đầu")]
        public string Symptom { get; set; }
        [Required]
        [Display(Name ="Bác sĩ")]
        public int DoctorId { get; set; }
    }
}
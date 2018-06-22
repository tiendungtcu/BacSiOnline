using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Patient.Models
{
    public class OrderViewModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name ="Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        public string Title { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        // Syndrome array
        [Required]
        [Display(Name = "Triệu chứng")]
        public int[] Symptoms { get; set; }

        public string Notes { get; set; }

        // Clinical Params
        [Range(40, 160, ErrorMessage = "Chiều cao trong khoảng 40-160")]
        [Display(Name = "Chiều cao")]
        public float Height { get; set; }

        [Range(40, 160, ErrorMessage = "Cân nặng trong khoảng 40-160")]
        public float Weight { get; set; }

        [Range(40, 160, ErrorMessage = "Huyết áp trong khoảng 40-160")]
        public float HighPressure { get; set; }

        [DataType(DataType.Date)]
        public DateTime HighPressureDate { get; set; }

        [Range(40, 160, ErrorMessage = "Huyết áp trong khoảng 40-160")]
        public float LowPressure { get; set; }

        [DataType(DataType.Date)]
        public DateTime LowPressureDate { get; set; }

        [Range(40, 160,ErrorMessage = "Nhịp tim trong khoảng 40-160")]
        public int HeartBeat { get; set; }

        [DataType(DataType.Date)]
        public DateTime HeartBeatDate { get; set; }

        // paraclinical paramId
        public int[] ParamIds { get; set; }

        // paraclinical Params
        public float[] ParaclinicalParams { get; set; }

        // paraclinical param measured date
        [DataType(DataType.Date)]
        public DateTime[] MeasuredDates { get; set; }  
        
        public int AddParam { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tm.Data.ViewModels.Patient
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
        [Display(Name = "Chiều cao")]
        [Required(ErrorMessage = "Chưa nhập chiều cao")]
        [Range(40, 160, ErrorMessage = "Chiều cao trong khoảng 40-160")]
        public float Height { get; set; }

        [Display(Name = "Cân nặng")]
        [Required(ErrorMessage = "Chưa nhập cân nặng")]
        [Range(40, 160, ErrorMessage = "Cân nặng trong khoảng 40-160")]
        public float Weight { get; set; }

        [Display(Name = "Huyết áp tâm trương")]
        [Required(ErrorMessage = "Chưa nhập huyết áp tâm trương")]
        [Range(40, 160, ErrorMessage = "Huyết áp trong khoảng 40-160")]
        public float HighPressure { get; set; }

        [DataType(DataType.Date)]
        public DateTime HighPressureDate { get; set; }

        [Display(Name = "Huyết áp tâm thu")]
        [Required(ErrorMessage = "Chưa nhập huyết áp tâm thu")]
        [Range(40, 160, ErrorMessage = "Huyết áp trong khoảng 40-160")]
        public float LowPressure { get; set; }

        [DataType(DataType.Date)]
        public DateTime LowPressureDate { get; set; }

        [Display(Name = "Nhịp tim")]
        [Required(ErrorMessage = "Chưa nhập nhịp tim")]
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
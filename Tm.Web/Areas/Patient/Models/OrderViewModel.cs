using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Patient.Models
{
    public class OrderViewModel
    {
        [Required]
        public string Title { get; set; }

        // Syndrome array
        [Required]
        public int[] Symptoms { get; set; }

        public string Notes { get; set; }

        // Clinical Params
        public float Height { get; set; }

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

        // paraclinical Params
        public float[] ParaclinicalParams { get; set; }

        // paraclinical param measured date
        [DataType(DataType.Date)]
        public DateTime[] MeasuredDates { get; set; }  
        

        public int AddParam { get; set; }
    }
}
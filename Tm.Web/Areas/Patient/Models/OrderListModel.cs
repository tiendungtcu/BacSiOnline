﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Patient.Models
{
    public class OrderListModel
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Bác sĩ pt")]
        public int[] Doctor { get; set; }

        [Display(Name = "Tình trạng")]
        public string Status { get; set; }

        [Display(Name = "Nội dung")]
        public string DiagnosisNote { get; set; }

        
    }
}
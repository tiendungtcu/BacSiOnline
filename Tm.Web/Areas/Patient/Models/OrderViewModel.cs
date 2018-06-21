using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Patient.Models
{
    public class OrderViewModel
    {
        public string Title { get; set; }

        public DateTime ThisDay { get; set; }

        public int[] Symptoms { get; set; }

        public float Height { get; set; }

        public float Weight { get; set; }



        public float Cholestron { get; set; }

        public DateTime CholDate { get; set; }
        
        public float GptIndex { get; set; }

        public DateTime GptDate { get; set; }


        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Doctor
{
    public class DoctorPatientModel
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string FullAddress { get; set; }
        public string AssurenceCard { get; set; }
        public DateTime LastOrder { get; set; }
    }
}

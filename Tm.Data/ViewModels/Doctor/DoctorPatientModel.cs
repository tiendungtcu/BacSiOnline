using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Doctor
{
    public class DoctorPatientModel
    {
        [Display(Name = "Mã B.Nhân")]
        public int PatientId { get; set; }
        [Display(Name = "Tên bệnh nhân")]
        public string PatientName { get; set; }
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }
        [Display(Name = "Địa chỉ")]
        public string FullAddress { get; set; }
        [Display(Name = "BHYT")]
        public string AssurenceCard { get; set; }
        [Display(Name = "Bệnh án gần đây nhất")]
        public DateTime LastOrder { get; set; }
    }
}

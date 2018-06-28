using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Doctor
{
    public class WaitingOrderModel
    {
        [Display(Name = "Mã B.án")]
        public long Id { get; set; }

        [Display(Name = "Mã B.Nhân ")]
        public int? PatientId { get; set; }

        [Display(Name = "Tên bệnh nhân")]
        public string PatientName { get; set; }

        [Display(Name = "Tuổi")]
        public int Age { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Ngày gửi")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
    }
}

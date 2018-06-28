using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Doctor
{
    public class PatientOrderDetail
    {
        [Display(Name = "Mã B.Nhân")]
        public int PatienId { get; set; }

        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Họ tên")]
        public string PatientName { get; set; }
        [Display(Name = "Tuổi")]
        public int Age { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "CMND")]
        public string IdentityCard { get; set; }
        [Display(Name = "Thẻ bảo hiểm")]
        public string AssurenceCard { get; set; }
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string Avatar { get; set; }
        [Display(Name = "Lần đăng nhập cuối")]
        public DateTime? LastLogin { get; set; }
        public IEnumerable<OrderListModel> Orders { get; set; }
    }
}

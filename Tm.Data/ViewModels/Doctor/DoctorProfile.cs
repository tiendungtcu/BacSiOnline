using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Doctor
{
    public class DoctorProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DoB { get; set; }

        public IList<AddressDetail> Addresses { get; set; }

        [StringLength(13, ErrorMessage = "Chứng minh thư có tối đa 13 ký tự")]
        public string IdentityCard { get; set; }

        public string Major { get; set; }
        public string Avatar { get; set; }

        [MaxLength(11, ErrorMessage = "Số điện thoại tối đa 11 ký tự.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Chưa đúng định dạng Email")]
        public string Email { get; set; }
        public ChangeUserPasswordViewModel Password { get; set; }
    }
}

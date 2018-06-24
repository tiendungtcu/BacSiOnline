using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Patient
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }
        public IList<AddressDetail> Addresses { get; set; }        
        public string IdentityCard { get; set; }
        public string AssuranceCard { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }


    }
}

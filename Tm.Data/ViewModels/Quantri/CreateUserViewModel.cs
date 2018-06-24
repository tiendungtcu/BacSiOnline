using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tm.Data.ViewModels.Quantri
{
    public class CreateUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string FullName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        public int? Role { get; set; }
    }
}
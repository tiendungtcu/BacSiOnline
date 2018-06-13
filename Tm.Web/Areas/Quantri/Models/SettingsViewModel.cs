using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TM.Web.Areas.Quantri.Models
{
    public class SettingsViewModel
    {
        [Required]
        public string ThemeSetting { get; set; }

        [Required]
        public string AppTitle { get; set; }

        [Required]
        public byte GioBaoBan { get; set; }

        [Required]
        public byte PhutBaoBan { get; set; }

        [Required]
        public string FooterLine { get; set; }

    }
}
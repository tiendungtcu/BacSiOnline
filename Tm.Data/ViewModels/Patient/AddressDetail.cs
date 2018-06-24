using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Patient
{
    public class AddressDetail
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Address { get; set; }
        public int? WardId { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
        public bool? Status { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public string WardName { get; set; }
    }
}

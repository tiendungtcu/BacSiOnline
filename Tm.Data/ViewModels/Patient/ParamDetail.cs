using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels.Patient
{
    public class ParamDetail
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public double? Value { get; set; }
        public string Unit { get; set; }
        public DateTime? MeasuredDate { get; set; }

    }
}

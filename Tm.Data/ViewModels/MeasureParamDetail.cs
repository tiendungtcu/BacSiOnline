using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels
{
    public class MeasureParamDetail
    {
        
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Type { get; set; }
        public string TypeName { get; set; }
        public Nullable<bool> Status { get; set; }

    }
}

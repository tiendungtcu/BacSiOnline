//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tm.Data.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TM_MeasureParam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TM_MeasureParam()
        {
            this.TM_OrderParam = new HashSet<TM_OrderParam>();
        }
    
        public short Id { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TM_OrderParam> TM_OrderParam { get; set; }
        public virtual TM_ParamType TM_ParamType { get; set; }
    }
}

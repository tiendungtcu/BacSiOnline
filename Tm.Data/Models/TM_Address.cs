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
    
    public partial class TM_Address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TM_Address()
        {
            this.UserAddresses = new HashSet<UserAddress>();
        }
    
        public int Id { get; set; }
        public Nullable<int> WardId { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Ward Ward { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}

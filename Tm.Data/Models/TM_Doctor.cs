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
    
    public partial class TM_Doctor
    {
        public int Id { get; set; }
        public string Major { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual TM_Users TM_Users { get; set; }
    }
}

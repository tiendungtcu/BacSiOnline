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
    
    public partial class TM_Notification
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<byte> Type { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Link { get; set; }
        public Nullable<int> ReceiverId { get; set; }
    
        public virtual TM_NotifyType TM_NotifyType { get; set; }
        public virtual TM_Users TM_Users { get; set; }
    }
}

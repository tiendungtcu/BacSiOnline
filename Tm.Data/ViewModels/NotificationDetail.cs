using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tm.Data.ViewModels
{
    public class NotificationDetail
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public byte? Type { get; set; }
        public string TypeName { get; set; }
        public string Receiver { get; set; }
        public int? ReceiverId { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Link { get; set; }      
    }
}

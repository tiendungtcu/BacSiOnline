using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;

namespace Tm.Data.Functions
{
    public class CommonDao
    {
        protected BACSIOIEntities db;
        public CommonDao()
        {
            db = new BACSIOIEntities();
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
    }
}

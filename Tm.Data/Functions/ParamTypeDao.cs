using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tm.Data.Models;
using Tm.Data.ViewModels;

namespace Tm.Data.Functions
{
    public class ParamTypeDao:CommonDao
    {
        public IEnumerable<ParamTypeDetail> ListAll()
        {
            return db.TM_ParamType
                        .Select(x => new ParamTypeDetail()
                        {
                            Id = x.Id,
                            TypeName = x.TypeName
                        })
                        .OrderBy(x => x.Id);                   
        }
        public TM_ParamType Find(int Id)
        {
            return db.TM_ParamType.Find(Id);
        }
        public string FindName(int? Id)
        {
            return db.TM_ParamType.Find(Id).TypeName;
        }
    }
}

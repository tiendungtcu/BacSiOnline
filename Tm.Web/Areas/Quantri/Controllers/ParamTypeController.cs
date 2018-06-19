using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class ParamTypeController : Controller
    {
        // List all paramtype 
        public JsonResult ListAll()
        {
            var entity = new ParamTypeDao().ListAll();
            return Json(entity, JsonRequestBehavior.AllowGet);
        }
        public String FindName(int id)
        {
            return new ParamTypeDao().Find(id).TypeName;
        }
    }
}
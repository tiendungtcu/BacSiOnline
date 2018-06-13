using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class WardController : Controller
    {
        public JsonResult WardDropdown(int disid, string term, string q, string _type = "query")
        {
            var entities = new WardDao().Search(disid, term).Select(x => new { id = x.Id, text = x.Type + " " + x.Name, disabled = x.IsDeleted == true ? true : false });
            return Json(new { results = entities }, JsonRequestBehavior.AllowGet);
        }
    }
}
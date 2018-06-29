using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using TM.Web.Areas.Quantri.Controllers;
using TM.Web.Controllers;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class DistrictController : CommonBaseController
    {
        /// <summary>
        /// Lấy danh sách quận nạp vào Select2
        /// </summary>
        /// <returns></returns>
        public JsonResult DistrictDropdown(int proid,string term, string q, string _type = "query")
        {
            var symptoms = new DistrictDao().Search(proid,term).Select(x => new { id = x.Id, text = x.Type + " " + x.Name, disabled = x.IsDeleted == true ? true : false });
            return Json(new { results = symptoms }, JsonRequestBehavior.AllowGet);
        }
    }
}
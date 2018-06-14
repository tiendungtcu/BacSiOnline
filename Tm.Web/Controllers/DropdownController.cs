using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Controllers
{
    public class DropdownController : Controller
    {

        // Provinces dropdown list
        public JsonResult ProvinceDropdown(string term, string q, string _type = "query")
        {
            var symptoms = new ProvinceDao().Search(term).Select(x => new { id = x.Id, text = x.Type + " " + x.Name, disabled = x.IsDeleted == true ? true : false });
            return Json(new { results = symptoms }, JsonRequestBehavior.AllowGet);
        }

        // District dropdown list
        public JsonResult DistrictDropdown(int proid, string term, string q, string _type = "query")
        {
            var symptoms = new DistrictDao().Search(proid, term).Select(x => new { id = x.Id, text = x.Type + " " + x.Name, disabled = x.IsDeleted == true ? true : false });
            return Json(new { results = symptoms }, JsonRequestBehavior.AllowGet);
        }

        // Wards dropdown list 
        public JsonResult WardDropdown(int disid, string term, string q, string _type = "query")
        {
            var entities = new WardDao().Search(disid, term).Select(x => new { id = x.Id, text = x.Type + " " + x.Name, disabled = x.IsDeleted == true ? true : false });
            return Json(new { results = entities }, JsonRequestBehavior.AllowGet);
        }

        // Symptoms Dropdown list
        public JsonResult SymptomDropdown(string term, string q, string _type = "query")
        {
            var symptoms = new SymptomDao().Search(term).Select(x => new { id = x.Id, text = x.Name, disabled = x.Status == true ? false : true });
            return Json(new { results = symptoms }, JsonRequestBehavior.AllowGet);
        }
    }
}
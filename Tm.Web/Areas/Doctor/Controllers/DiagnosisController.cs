using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TM.Web.Areas.Doctor.Controllers
{
    public class DiagnosisController : Controller
    {
        // GET: Doctor/Diagnosis
        public ActionResult Index()
        {
            return View();
        }
    }
}
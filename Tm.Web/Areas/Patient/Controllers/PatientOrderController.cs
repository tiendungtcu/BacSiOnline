using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TM.Web.Areas.Patient.Controllers
{
    public class PatientOrderController : Controller
    {
        // GET: Patient/PatientOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}
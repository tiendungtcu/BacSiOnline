using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using TM.Web.Areas.Patient.Models;

namespace TM.Web.Areas.Patient.Controllers
{
    public class PatientOrderController : Controller
    {
        // GET: Patient/PatientOrder
        public ActionResult Index()
        {
            return View();
        }

        // GET: Patient/PatientOrder/Create
        public ActionResult Create()
        {
            ViewBag.Symptom = "symptom";
            ViewBag.SymptomList = new MeasureParamDao().ListAll(2);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (OrderViewModel model)
        {

            if (ModelState.IsValid)
            {

                return Json(model);
            }
            ViewBag.SymptomList = new MeasureParamDao().ListAll();
            
            return View(model);
        }
    }
}
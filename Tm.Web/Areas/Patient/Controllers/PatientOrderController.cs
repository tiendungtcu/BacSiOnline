using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            ViewBag.DataNew = "chưa có gì";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ThisDay = DateTime.Now;

            }
            ViewBag.DataNew = model;
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TM.Web.Areas.Doctor.Controllers
{
    public class DefaultController : DoctorBaseController
    {
        // GET: Doctor/Home
        public ActionResult Default()
        {
            return View();
        }
    }
}
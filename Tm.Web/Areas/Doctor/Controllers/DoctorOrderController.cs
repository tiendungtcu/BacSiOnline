using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Areas.Doctor.Controllers
{
    public class DoctorOrderController : Controller
    {
        // GET: Doctor/DoctorOrder
        public ActionResult Index()
        {
            // Get list of all Orders this doctor responds
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            var orders = new OrderDao().ListOrdersByDoctor(doctorId);
            //return Json(orders,JsonRequestBehavior.AllowGet);
            return View(orders);
        }
    }
}
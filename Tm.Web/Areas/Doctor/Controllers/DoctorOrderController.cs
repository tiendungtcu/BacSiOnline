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
        // GET: Doctor/DoctorOrder - show all order by doctor
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

        // GET: Show order detail
        public ActionResult Detail(long? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "DoctorOrder", new { Area = "Doctor" });
            }
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }

            List<long> orderList = new List<long>();

            // Get list of patients belong to this doctor
            var patients = new UserDao().GetPatientsFromDoctor(doctorId);

            // Get list of orders from all patients
            foreach (var item in patients)
            {
                if (item.HasValue)
                {
                    // Get all orders from each patient
                    var orders = new OrderDao().GetOrderIdTitle((int)item);
                    foreach (var order in orders)
                    {
                        if (order.Id>0)
                        {
                            orderList.Add(order.Id);
                        }
                    }
                }
                
            }

            ViewBag.OrderList = new SelectList(orderList, id);
            // Get order detail by id
            var model = new OrderDao().GetDetail((long)id);
            if (model == null)
            {
                return RedirectToAction("Index", "DoctorOrder", new { Area = "Doctor" });
            }
            //return Json(model,JsonRequestBehavior.AllowGet);                   
            return View(model);
        }

        // POST: Diagnosis an order
        [HttpPost]
        public ActionResult Diagnosis(long Id, string DiagnosisNotes)
        {
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            if (new DoctorDao().DiagnosOrder(doctorId, Id, DiagnosisNotes))
            {
                return RedirectToAction("Detail", "DoctorOrder", new { Area = "Doctor", Id });
            }
            
            return Json(new { id = Id, diagnosis = DiagnosisNotes });
        }

        // GET: Show waiting list
        public ActionResult WaitingList()
        {
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            var model = new OrderDao().GetWaitingList(doctorId);
            return View(model);
        }

        //GET: Statistic for all order
        public ActionResult Statistic()
        {
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            var model = new OrderDao().GetAllDoctorOrders(doctorId);
            //return Json(model,JsonRequestBehavior.AllowGet);
            return View(model);
        }
    }
}
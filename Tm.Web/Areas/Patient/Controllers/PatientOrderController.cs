using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels.Patient;

namespace TM.Web.Areas.Patient.Controllers
{
    public class PatientOrderController : Controller
    {
        // Show history of orders of a patient
        public ActionResult History()
        {
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                patienId = 1021;
            }
            var models = new OrderDao().GetHistories(patienId);
            return View(models);
        }
        // Get detail of an order       
        public ActionResult Detail(long? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "PatientOrder", new { Area = "Patient" });
            }
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                patienId = 1021;
            }           
            var model = new OrderDao().GetDetail((long)id,patienId);
            if (model==null)
            {
                return RedirectToAction("Index", "PatientOrder", new { Area = "Patient" });
            }
            //return Json(model,JsonRequestBehavior.AllowGet);                   
            return View(model);
        }
        // Patient see a list of order that he submitted before
        // GET: Patient/PatientOrder
        public ActionResult Index(int? typeFilter)
        {
            // Get list of all patient Orders
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                patienId = 1021;
            }
            var orders = new OrderDao().ListOrdersByPatient(patienId);
            if (typeFilter==2)
            {
                orders = orders.Where(o => o.Status.Equals("Chưa chẩn đoán"));
            }
            if (typeFilter==1)
            {
                orders = orders.Where(o => o.Status.Equals("Đã chẩn đoán"));
            }
            IList<SelectListItem> statusList = new List<SelectListItem>
            {
                new SelectListItem{Text = "--Tất cả--", Value = "0"},
                new SelectListItem{Text = "Đã chẩn đoán", Value = "1"},
                new SelectListItem{Text = "Chưa chẩn đoán", Value = "2"},
            };

            ViewBag.StatusFilter = statusList;
            ViewBag.Message = TempData["Message"] != null ? TempData["Message"] : string.Empty;
            return View(orders);
        }

        // GET: Patient/PatientOrder/Create
        public ActionResult Create()
        {
            ViewBag.Symptom = "symptom";
            ViewBag.SymptomList = new MeasureParamDao().ListAll(2);
            return View();
        }
        // Patient create order and add symptoms, measured params
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (OrderViewModel model)
        {
            ViewBag.SymptomList = new MeasureParamDao().ListAll(2);
            if (ModelState.IsValid)
            {
                int curUserId = User.Identity.GetUserId<int>(); //Get current user Id
                if (curUserId<=0)
                {
                    curUserId = 1021;
                }

                TM_Order order = new TM_Order();
                order.OrderDate = DateTime.Now;
                order.PatientId = curUserId;
                order.Status = true;
                order.Title = model.Title;
                order.Notes = model.Notes;
                // Create an order
                long orderId = new OrderDao().Create(order);
                // add symptoms to order
                for (int i = 0; i < model.Symptoms.Length; i++)
                {
                    // 
                    if(!(new OrderDao().AddSymptom(order.Id, model.Symptoms[i])))
                    {
                        ModelState.AddModelError("", "Không thêm được triệu chứng thứ"+(i+1));
                        return View(model);
                    }
                        
                }
                // add clinical params to order
                // add Height param
                if (!new OrderDao().AddParamByName(order.Id,"Height",model.Height,DateTime.Now))
                {
                    ModelState.AddModelError("", "Không thêm được chiều cao");
                    return View(model);
                }
                // add Weight param
                if (!new OrderDao().AddParamByName(order.Id, "Weight", model.Weight, DateTime.Now))
                {
                    ModelState.AddModelError("", "Không thêm được cân nặng");
                    return View(model);
                }
                // add Weight param
                if (!new OrderDao().AddParamByName(order.Id, "LowPressure", model.LowPressure, model.LowPressureDate))
                {
                    ModelState.AddModelError("", "Không thêm được huyết áp tâm thu");
                    return View(model);
                }
                // add Weight param
                if (!new OrderDao().AddParamByName(order.Id, "HighPressure", model.HighPressure, model.HighPressureDate))
                {
                    ModelState.AddModelError("", "Không thêm được huyết áp tâm trương");
                    return Json(new { loi = "HighPressure" });
                    //return View(model);
                }
                // add Weight param
                if (!new OrderDao().AddParamByName(order.Id, "HeartBeat", model.HeartBeat, model.HeartBeatDate))
                {
                    ModelState.AddModelError("", "Không thêm được nhịp tim");
                    return Json(new { loi = "HeartBeat" });
                    //return View(model);
                }
                
                // Add paraclinical params to order
                for (int j = 0; j < model.ParamIds.Length; j++)
                {
                    if (!new OrderDao().AddParamById(orderId, (short)model.ParamIds[j], model.ParaclinicalParams[j], model.MeasuredDates[j]))
                    {
                        ModelState.AddModelError("", "Không thêm được thông số cận lâm sàng");
                        return Json(new { loi = "paraclinical" });
                        //return View(model);
                    }
                }

                // Find a doctor for this order
                int doctorId = new DoctorDao().FindDoctorForPatient(curUserId);
                if (doctorId <=0)
                {
                    ModelState.AddModelError("", "Không tìm thấy bác sĩ nào trên hệ thống");
                    return Json(new { loi = "doctor not found" });
                    //return View(model);
                }
                // Assign doctor to order
                if (!new OrderDao().AssignDoctorToOrder(doctorId,orderId,curUserId))
                {
                    ModelState.AddModelError("", "Không thể gửi yêu cầu này cho bác sĩ");
                    return Json(new { loi = "asign doctor for an order",doctor=doctorId,patient = curUserId });
                    //return View(model);
                }
                TempData["Message"] = "Tạo bệnh án thành công";
                return RedirectToAction("Index","PatientOrder",new { Area = "Patient" });
            }
            
            return View(model);
        }

    }
}
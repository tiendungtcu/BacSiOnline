using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels.Patient;
using TM.Web.Models;

namespace TM.Web.Areas.Patient.Controllers
{
    public class PatientOrderController : Controller
    {

        // GET: history of orders of a patient
        public ActionResult History()
        {
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                return RedirectToAction("Login","Account",new { Area=""});
            }
            var models = new OrderDao().GetHistories(patienId);
            //return Json(models,JsonRequestBehavior.AllowGet);   
            return View(models);
        }
       
        // GET: detail of an order       
        public ActionResult Detail(long? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "PatientOrder", new { Area = "Patient" });
            }
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }           
            var model = new OrderDao().GetDetail((long)id);
            if (model==null)
            {
                return RedirectToAction("Index", "PatientOrder", new { Area = "Patient" });
            }
            //return Json(model,JsonRequestBehavior.AllowGet);                   
            return View(model);
        }

        // GET: lists of order that patient submitted with filter
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
            // List all paraclinical params
            ViewBag.Symptom = "symptom";
            ViewBag.SymptomList = new MeasureParamDao().ListAll(2);
            return View();
        }

        // POST: Patient create order and add symptoms, measured params
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (OrderViewModel model)
        {

            // List all paraclinical params
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

                // Notify to doctor
                TM_Notification noty = new TM_Notification();
                noty.Title = "Yêu cầu chẩn đoán từ " + User.Identity.GetFullName();
                noty.Link = (int)order.Id;
                noty.CreatedDate = DateTime.Now;
                noty.Contents = "Bệnh nhân gửi yêu cầu chẩn đoán cho bác sĩ";
                noty.Type = 1; // yêu cầu chẩn đoán
                noty.ReceiverId = doctorId;
                noty.Status = false;
                var notiResult = new NotificationDao().Create(noty);
                if (notiResult<1)
                {
                    ModelState.AddModelError("", "Không tìm thấy bác sĩ nào trên hệ thống");
                    return Json(new { loi = "Không thêm được thông báo" });
                }
                TempData["Message"] = "Tạo bệnh án thành công";
                return RedirectToAction("Index","PatientOrder",new { Area = "Patient" });
            }
            
            return View(model);
        }

        // GET: Health Chart, can choose Params
        public ActionResult ShowChart(int[] arguments)
        {
            
            return View();
        }

        // AJAX: Get Chart datas
        public JsonResult GetChartData()
        {
            // Find current user Id
            int patienId = User.Identity.GetUserId<int>(); //Get current user Id
            if (patienId <= 0)
            {
                patienId = 1021;
            }

            // Get list of all orders of current user
            var models = new OrderDao().GetHistories(patienId).ToList();
            List<object> dataset = new List<object>();
            var labels = new List<string>();
            var lowPress = new List<int>();
            var highPress = new List<int>();
            var heartBeat = new List<int>();
            var gpt = new List<int>();
            var insulin = new List<int>();
            var cholesteron = new List<int>();
            // Loop through all order to add datas
            for(var i=0; i< models.Count();i++)
            {
                labels.Add(((DateTime)models[i].CreatedDate).ToString("dd/MM/yy"));
                foreach (var param in models[i].ClinicalParams)
                {
                    if (param.CodeName.Equals("HeartBeat"))
                    {
                        heartBeat.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("LowPressure"))
                    {
                        lowPress.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("HighPressure"))
                    {
                        highPress.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("GPT"))
                    {
                        lowPress.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("Insulin"))
                    {
                        highPress.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("Cholesteron"))
                    {
                        highPress.Add((int)param.Value);
                    }

                }
                foreach (var param in models[i].ParaclinicalParams)
                {                   
                    if (param.CodeName.Equals("GPT"))
                    {
                        gpt.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("Insulin"))
                    {
                        insulin.Add((int)param.Value);
                    }
                    if (param.CodeName.Equals("Cholesteron"))
                    {
                        cholesteron.Add((int)param.Value);
                    }

                }

                // if one has null value, add zero to it
                if (heartBeat.Count< i+1)
                {
                    heartBeat.Add(0);
                }
                if (lowPress.Count < i + 1)
                {
                    lowPress.Add(0);
                }
                if (highPress.Count < i + 1)
                {
                    highPress.Add(0);
                }
                if (gpt.Count < i + 1)
                {
                    gpt.Add(0);
                }
                if (cholesteron.Count < i + 1)
                {
                    cholesteron.Add(0);
                }
                if (insulin.Count < i + 1)
                {
                    insulin.Add(0);
                }
            }
            dataset.Add(labels);
            dataset.Add(heartBeat);
            dataset.Add(lowPress);
            dataset.Add(highPress);
            dataset.Add(gpt);
            dataset.Add(insulin);
            dataset.Add(cholesteron);
            return Json(new { d = dataset });

        }

        // GET: Show Dashboard
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}
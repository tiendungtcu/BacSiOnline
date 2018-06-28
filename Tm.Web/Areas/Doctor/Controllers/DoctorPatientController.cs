using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Areas.Doctor.Controllers
{
    public class DoctorPatientController : DoctorBaseController
    {
        // GET: Doctor/DoctorPatient
        public ActionResult History(int? id)
        {
            
            int doctorId = User.Identity.GetUserId<int>(); //Get current user Id
            if (doctorId <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            var patients = new UserDao().GetPatientsFromDoctor(doctorId);
            List<int> patientList = new List<int>();
            foreach (var item in patients)
            {
                if (item.HasValue)
                {
                    patientList.Add((int)item);
                }
            }
            
            int patientId = id.HasValue ? (int)id : patientList[0];
            ViewBag.PatientList = new SelectList(patientList, patientId);
            var models = new OrderDao().GetHistories(patientId);
            return View(models);
        }

        // GET: Get Patient detail
        public ActionResult Detail(int id)
        {
            
            int doctor = User.Identity.GetUserId<int>();
            if (doctor<1)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            ViewBag.PatientList = new SelectList(new PatientDao().GetDoctorPatientsId(doctor),id);
            var model = new PatientDao().GetPatientDetail(id);
            //return Json(model,JsonRequestBehavior.AllowGet);           
            return View(model);
        }

        //GET: Get patients list
        public ActionResult Index()
        {
            int doctor = User.Identity.GetUserId<int>();
            if (doctor < 1)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            var model = new PatientDao().GetPatientsDetail(doctor);
            //return Json(model, JsonRequestBehavior.AllowGet);
            return View(model);
        }
    }
}
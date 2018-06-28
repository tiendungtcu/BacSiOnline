using Microsoft.AspNet.Identity;
using System;
using System.Text;
using System.Web.Mvc;
using Tm.Data.Common;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels.Reception;
using TM.Web.Areas.Quantri.Controllers;
using TM.Web.Models;

namespace TM.Web.Areas.Reception.Controllers
{
    public class RegisterPatientController : QuantriBaseController
    {
        // GET: Reception/Home
        public ActionResult Index()
        {
            ViewBag.UserName = TempData["UserName"];
            ViewBag.Password = TempData["Password"];
            return View();
        }
        // GET: Reception/Home
        public ActionResult Create()
        {
            return View();
        }
        // POST: Reception/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReceptionViewModel entity)
        {

            if (string.IsNullOrWhiteSpace(entity.FullName))
            {
                ViewBag.Error = "Chưa nhập họ tên bệnh nhân";
                return View(entity);
            }
            // Create 
            string newstr = Utilities.RemoveUnicode(entity.FullName);
            string[] split = newstr.Split(' ');
            StringBuilder builder = new StringBuilder();
            if (split.Length > 2)
            {
                if (!string.IsNullOrWhiteSpace(split[2]))
                {
                    builder.Append(split[2]);
                }
            }
            if (split.Length > 1)
            {
                if (!string.IsNullOrWhiteSpace(split[1]))
                {
                    builder.Append(split[1]);
                }
            }
            builder.Append('.');
            if (!string.IsNullOrWhiteSpace(split[0]))
            {
                builder.Append(split[0]);
            }
            if (entity.BirthYear > 0)
            {
                builder.Append(entity.BirthYear);
            }          
            string username = builder.ToString().ToLower();
            TempData["UserName"] = username; ;
            // Create password
            string password = "123456";
            TempData["Password"] = password;           
            //long temp = 0;
            if (entity.PhoneNumber.Length<10 || !IsNumbers(entity.PhoneNumber))
            {
                ViewBag.Error = "Số điện thoại phải là số có độ dài >=10";
                return View(entity);
            }
            if (UserManager.FindByName(entity.PhoneNumber)!=null)
            {
                ViewBag.Error = "Số điện thoại này đã đăng ký";
                return View(entity);
            }
            // Create new User
            var user = new ApplicationUser
            {
                UserName = entity.PhoneNumber,
                PhoneNumber = entity.PhoneNumber,
                FullName = entity.FullName,
                Email = username + "@gmail.com",
                Gender = entity.Gender,
                DateOfBirth = new DateTime(entity.BirthYear, 1, 1),
                LastLogin = DateTime.Now
            };
            var createdUser = UserManager.Create(user, password);
            
            // Assign user to role PATIENT_GROUP
            IdentityResult createdRole = null;
            if (createdUser.Succeeded)
            {
                // Assign user to Patien role
                createdRole = UserManager.AddToRole(user.Id, "PATIENT_GROUP");
            }
            else
            {
                ViewBag.Error = "Không tạo được user";
                return View(entity);

            }
            // Update database
            ReceptionRegisterSp_Result result = null;
            if (createdRole.Succeeded)
            {
                // Create input model
                SpReceptionRegisterViewModel model = new SpReceptionRegisterViewModel();
                model.UserId = user.Id;
                model.Address = entity.Address;
                model.WardId = entity.WardId;
                model.Syptom = entity.Symptom;
                model.DoctorId = entity.DoctorId;               
                result = new ReceptionDao().CreateNewOrder(model);
                ViewBag.Result = result;
            }
            else
            {
                ViewBag.Error = "Không tạo được Role";
                return View(entity);
            }
            if (result==null)
            {
                ViewBag.Error = "Không tạo được Order";
                return View(entity);
            }
            // Send Notify to doctor
            // Notify to doctor
            TM_Notification noty = new TM_Notification();
            noty.Title = "Yêu cầu chẩn đoán từ " + User.Identity.GetFullName();
            noty.Link = (int)result.OrderId;
            noty.CreatedDate = DateTime.Now;
            noty.Contents = "Bệnh nhân gửi yêu cầu chẩn đoán cho bác sĩ";
            noty.Type = 1; // yêu cầu chẩn đoán
            noty.ReceiverId = entity.DoctorId;
            noty.Status = false;
            var notiResult = new NotificationDao().Create(noty);
            if (notiResult < 1)
            {
                ModelState.AddModelError("", "Không thêm được thông báo");
                return Json(new { loi = "Không thêm được thông báo" });
            }
            return RedirectToAction("Index");
            
        }

        // Check if string is all numbers
        public bool IsNumbers(string phone)
        {
            phone = phone.Trim();
            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i]-'0'<0 || phone[i]-'0'>9)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
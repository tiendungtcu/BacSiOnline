using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using TM.Web.Areas.Quantri.Controllers;
using TM.Web.Areas.Reception.Models;
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
            string newstr = RemoveUnicode(entity.FullName);
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
                FullName = entity.FullName,
                Email = username + "@gmail.com",
                Gender = entity.Gender,
                DateOfBirth = new DateTime(entity.BirthYear, 1, 1)
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

        // Bỏ dấu tiếng việt
        public string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
    }
}
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
    public class DefaultController : QuantriBaseController
    {
        // GET: Reception/Home
        public ActionResult Index()
        {
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

            // Create Username
            string[] split = entity.FullName.Split(' ');
            StringBuilder builder = new StringBuilder();
            if (split.Length > 1)
            {
                if (!string.IsNullOrWhiteSpace(split[2]))
                {
                    builder.Append(split[2]);
                }
            }
            if (split.Length > 0)
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
            ViewBag.UserName = username;
            // Create password
            string password = "123456";
            ViewBag.Password = password;

            // Create new User
            var user = new ApplicationUser
            {
                UserName = entity.PhoneNumber,
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
          return View("Index");
            
        }
    }
}
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using Tm.Data.ViewModels.Patient;
using TM.Web.Controllers;
using TM.Web.Models;

namespace TM.Web.Areas.Reception.Controllers
{
    [Authorize(Roles = "RECEPTION_GROUP")]
    public class ReceptionProfileController : CommonBaseController
    {
        // Update user detail
        [HttpPost]
        public ActionResult Update(PatientProfile model, IList<AddressDetail> addr)
        {
            if (addr != null)
            {
                model.Addresses = addr; // Assign AddressDetail list to PatientProfile model
            }
            else
            {
                model.Addresses = null;
            }
            if (ModelState.IsValid)
            {
                int userid = User.Identity.GetUserId<int>();
                ApplicationUser user = UserManager.FindById(userid);
                if (user == null)
                {
                    ViewBag.Errors = "Không thấy lễ tân này này";
                    return RedirectToAction("Detail", "ReceptionProfile", new { Message = ProfileMessageId.Error });
                }

                // Update account data

                user.Email = model.Email;
                user.DateOfBirth = model.DoB;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Gender = model.Gender;
                var updateacc = UserManager.Update(user);
                if (!updateacc.Succeeded)
                {
                    ViewBag.Errors = "Không cập nhật được account";
                    return View("Detail", model);
                }

                // Update profile data
                TM_Patient patient = new PatientDao().FindByUserId(userid);
                if (patient == null)
                {
                    patient = new TM_Patient();
                    patient.UserId = userid;
                    patient.AssuranceCard = model.AssuranceCard;
                    patient.IdentityCard = model.IdentityCard;
                    if (new PatientDao().Insert(patient) < 1)
                    {
                        ViewBag.Errors = "Không thêm được profile";
                        return View("Detail", model);
                    }
                }
                else
                {
                    patient.AssuranceCard = model.AssuranceCard;
                    patient.IdentityCard = model.IdentityCard;
                    if (!new PatientDao().Update(patient))
                    {
                        ViewBag.Errors = "Không cập nhật được profile";
                        return View("Detail", model);
                    }
                }
                return RedirectToAction("Detail", new { message = ProfileMessageId.ChangeAccountSuccess });
            }
            return View("Detail", model);
        }

        // GET: Patient/PatienProfile
        public ActionResult Detail(ProfileMessageId? message)
        {
            ViewBag.Errors = message == ProfileMessageId.ChangeAccountSuccess ? "Cập nhật tài khoản thành công."
                : message == ProfileMessageId.AddAddressSuccess ? "Thêm địa chỉ thành công."
                : message == ProfileMessageId.ChangeAddressSuccess ? "Thay đổi địa chỉ thành công."
                : message == ProfileMessageId.Error ? "Lỗi không thực hiện được."
                : message == ProfileMessageId.ChangePasswordSuccess ? "Đổi mật khẩu thành công."
                : message == ProfileMessageId.ChangeProfileSuccess ? "Cập nhật thông tin thành công."
                : "";
            int userid = User.Identity.GetUserId<int>();
            if (userid <= 0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            //var model = new UserDao().GetAddresses(userid);
            var model = new UserDao().GetPatienProfile(userid);

            //return Json(model,JsonRequestBehavior.AllowGet);
            return View(model);
        }


        public enum ProfileMessageId
        {
            ChangeAddressSuccess,
            ChangePasswordSuccess,
            AddAddressSuccess,
            ChangeAccountSuccess,
            ChangeProfileSuccess,
            Error
        }
    }
}
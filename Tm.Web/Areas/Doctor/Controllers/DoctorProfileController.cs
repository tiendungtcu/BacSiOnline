using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.Models;
using Tm.Data.ViewModels;
using Tm.Data.ViewModels.Doctor;
using TM.Web.Areas.Quantri.Controllers;
using TM.Web.Controllers;
using TM.Web.Models;

namespace TM.Web.Areas.Doctor.Controllers
{
    public class DoctorProfileController : CommonBaseController
    {
        // POST: Update doctor detail
        [HttpPost]
        public ActionResult Update(DoctorProfile model, IList<AddressDetail> addr)
        {
            if (addr!=null)
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
                    ViewBag.Errors = "Không thấy bệnh nhân này";
                    return RedirectToAction("Detail", "DoctorProfile", new { Message = ProfileMessageId.Error });
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
                TM_Doctor doctor = new DoctorDao().FindByUserId(userid);
                if (doctor == null)
                {
                    doctor = new TM_Doctor();
                    doctor.UserId = userid;
                    doctor.Major = model.Major;
                    doctor.IdentityCard = model.IdentityCard;
                    if (new DoctorDao().Insert(doctor) < 1)
                    {
                        ViewBag.Errors = "Không thêm được profile";
                        return View("Detail", model);
                    }
                }
                else
                {
                    doctor.Major = model.Major;
                    doctor.IdentityCard = model.IdentityCard;
                    if (!new DoctorDao().Update(doctor))
                    {
                        ViewBag.Errors = "Không cập nhật được profile";
                        return View("Detail", model);
                    }
                }
                return RedirectToAction("Detail", new { message = ProfileMessageId.ChangeAccountSuccess });
            }
            return View("Detail",model);
        }

        // POST: Change doctor password
        [HttpPost]
        public async Task<JsonResult> ChangePasswordAsync(ChangeUserPasswordViewModel model)
        {
            // If modelstate isn't valid
            if (!ModelState.IsValid)
            {
                IList<string> errorMessages = new List<string>();


                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
                return Json(errorMessages);
            }

            // if current user isn't found
            int userid = User.Identity.GetUserId<int>();
            if (userid <= 0)
            {
                return Json(new { message = "Người dùng chưa đăng nhập" });
            }

            // change password
            var result = await UserManager.ChangePasswordAsync(userid, model.OldPassword, model.NewPassword);

            // if success
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userid);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: true);
                }
                return Json(new { message = "Đổi mật khẩu thành công" });
            }
            return Json(result.Errors);
        }

        // GET: View doctor detail
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
            var model = new UserDao().GetDoctorProfile(userid);
            return View(model);

        }

        // Message enumeration
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
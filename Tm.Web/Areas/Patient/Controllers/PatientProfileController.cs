using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Tm.Data.Functions;
using Tm.Data.ViewModels.Patient;
using TM.Web.Areas.Quantri.Controllers;

namespace TM.Web.Areas.Patient.Controllers
{
    public class PatientProfileController : QuantriBaseController
    {
        // Update user detail
        [HttpPost]
        public ActionResult Update(PatientProfile model)
        {
            return Json(model);
        }
        // Change password
        [HttpPost]
        public ActionResult ChangePassword(PatientProfile model)
        {
            return Json(model);
        }
        // GET: Patient/PatienProfile
        public ActionResult Detail()
        {
            int userid = User.Identity.GetUserId<int>();
            if (userid<=0)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            //var model = new UserDao().GetAddresses(userid);
            var model = new UserDao().GetPatienProfile(userid);

            //return Json(model,JsonRequestBehavior.AllowGet);
            return View(model);
            /*
            ApplicationUser user = UserManager.FindById(userid);
            var model = new PatientProfile
            {
                Id = userid,
                FullName = user.FullName,
                Gender = user.Gender,
                DoB = (DateTime)user.DateOfBirth,
                PhoneNumber = user.PhoneNumber

            }
            return View();
            */
        }
    }
}
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tm.Data.Functions;

namespace TM.Web.Controllers
{
    public class NotificationsController : Controller
    {
        // GET: Notification
        public ActionResult ListAll()
        {
            int userid = User.Identity.GetUserId<int>();
            var model = new NotificationDao().ListByUser(userid);
            return PartialView(model);
        }
        // Mark a notification has been viewed
        public ActionResult Viewed(int id)
        {
            if (new NotificationDao().HasBeenViewed(id))
            {
                var orderId = new NotificationDao().FindLink(id);
                return RedirectToAction("Detail", "PatientOrder", new { Area = "Patient", id= orderId });
            }
            return RedirectToAction("Index", "PatientOrder", new { Area = "Patient" });
        }
    }
}
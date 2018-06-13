using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM.Web.Models;
using Tm.Data.Models;

namespace TM.Web.Areas.Quantri.Controllers
{
    public class QuantriBaseController : Controller
    {
        protected MyIdentityDbContext context;
        protected ApplicationUserManager _userManager;
        protected ApplicationSignInManager _signInManager;
        public QuantriBaseController()
        {
            context = new MyIdentityDbContext();           
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public QuantriBaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public JsonResult NotifyError(string action)
        {
            return Json(new
            {
                isError = true,
                errorMsg = "Không thể thực hiện thao tác " + action + " !."
            });
        }

    }
}
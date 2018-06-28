using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TM.Web.Areas.Doctor.Controllers
{
    [Authorize(Roles ="DOCTOR_GROUP")]
    public class DoctorBaseController : Controller
    {

    }
}
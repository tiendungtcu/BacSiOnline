using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TM.Web.Areas.Patient.Controllers
{
    [Authorize(Roles ="PATIENT_GROUP")]
    public class PatientBaseController : Controller
    {

    }
}
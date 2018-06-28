using System.Web.Mvc;

namespace TM.Web.Areas.Patient
{
    public class PatientAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Patient";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // View orders by patient 
            context.MapRoute(
                 "patientvieworder", //name
                "yeu-cau-giai-dap/{action}", // url
                new { Area = "Patient", controller = "PatientOrder", action = "Index" , id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Patient.Controllers" }  //namespace
            );
            // View profile by patient
            context.MapRoute(
                 "patientviewprofile", //name
                "xem-thong-tin-ca-nhan/{action}", // url
                new { Area = "Patient", controller = "PatientProfile", action = "Detail", id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Patient.Controllers" }  //namespace
            );

            context.MapRoute(
                "Patient_default",
                "Patient/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
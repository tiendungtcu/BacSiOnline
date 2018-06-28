using System.Web.Mvc;

namespace TM.Web.Areas.Doctor
{
    public class DoctorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Doctor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // doctor views orders
            context.MapRoute(
                 "doctorvieworder", //name
                "bs-xem-yeu-cau/{action}", // url
                new { Area = "Doctor", controller = "DoctorOrder", action = "Index", id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Doctor.Controllers" }  //namespace
            );
            // doctor view patients
            context.MapRoute(
                 "doctorviewpatient", //name
                "bs-xem-benh-nhan/{action}", // url
                new { Area = "Doctor", controller = "DoctorPatient", action = "Index", id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Doctor.Controllers" }  //namespace
            );
            // View profile by patient
            context.MapRoute(
                 "doctorviewprofile", //name
                "bs-xem-thong-tin-ca-nhan/{action}", // url
                new { Area = "Doctor", controller = "DoctorProfile", action = "Detail", id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Doctor.Controllers" }  //namespace
            );
            context.MapRoute(
                "Doctor_default",
                "Doctor/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
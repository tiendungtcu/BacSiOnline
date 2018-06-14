using System.Web.Mvc;

namespace TM.Web.Areas.Reception
{
    public class ReceptionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Reception";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            // Register new Patient 
            context.MapRoute(
                 "registernewpatient", //name
                "dang-ky-benh-nhan/{action}", // url
                new { Area = "Reception", controller = "RegisterPatient", action = "Create" }, // defaults
                new[] { "TM.Web.Areas.Reception.Controllers" }  //namespace
            );

            context.MapRoute(
                "Reception_default",
                "Reception/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
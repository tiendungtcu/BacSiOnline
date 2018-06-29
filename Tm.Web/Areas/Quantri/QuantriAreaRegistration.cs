using System.Web.Mvc;

namespace TM.Web.Areas.Quantri
{
    public class QuantriAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Quantri";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            // View orders by patient 
            context.MapRoute(
                 "quantrihethong", //name
                "od-quantri-page/{action}", // url
                new { Area = "Quantri", controller = "Default", action = "Default", id = UrlParameter.Optional }, // defaults
                new[] { "TM.Web.Areas.Quantri.Controllers" }  //namespace
            );

            context.MapRoute(
                "Quantri_default",
                "Quantri/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
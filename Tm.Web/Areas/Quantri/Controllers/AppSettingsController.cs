using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Tm.Data.ViewModels.Quantri;

namespace TM.Web.Areas.Quantri.Controllers
{
    //[Authorize(Roles = "QuanTri")]
    public class AppSettingsController : Controller
    {
        // GET: Admin/ChangeSettings
        public ActionResult ChangeSettings()
        {
            // Khởi tạo danh sách các theme
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Kiểu theme mặc định", Value = "default"},
                new SelectListItem{Text = "Theme kiểu màu xanh da trời", Value = "metro-blue"},
                new SelectListItem{Text = "Theme kiểu màu xám", Value = "metro-gray"},
                new SelectListItem{Text = "Theme kiểu màu xanh lá", Value = "metro-green"},
                new SelectListItem{Text = "Theme kiểu màu cam", Value = "metro-orange"},
                new SelectListItem{Text = "Theme kiểu màu đỏ", Value = "metro-red"}

            };
            SettingsViewModel setting = new SettingsViewModel();
            setting.AppTitle = ConfigurationManager.AppSettings["AppTitle"];
            setting.FooterLine = ConfigurationManager.AppSettings["FooterLine"];
            setting.ThemeSetting = ConfigurationManager.AppSettings["ThemeSetting"];
            ViewBag.ThemesList = new SelectList(items, "Value", "Text", setting.ThemeSetting);
            return View(setting);
        }
        [HttpPost]
        // POST: Admin/SaveSettings
        public JsonResult SaveSettings(SettingsViewModel settings)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ConfigurationManager.AppSettings.Set("AppTitle", settings.AppTitle);
                    ConfigurationManager.AppSettings.Set("ThemeSetting", settings.ThemeSetting);
                    ConfigurationManager.AppSettings.Set("FooterLine", settings.FooterLine);
                    ConfigurationManager.AppSettings.Set("GioBaoBan", settings.GioBaoBan.ToString());
                    ConfigurationManager.AppSettings.Set("PhutBaoBan", settings.PhutBaoBan.ToString());
                    return Json(new { message = "success" });
                }
                catch (Exception e)
                {
                    return Json(new { messange = e.Message });
                }
            }
            return Json(new { message = "error" });
        }
    }
}
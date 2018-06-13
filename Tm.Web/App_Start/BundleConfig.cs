using System.Web;
using System.Web.Optimization;

namespace TM.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/mysite").Include(
                      "~/Content/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/mysite").Include(
                      "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Content/easyui/extensions/jquery.edatagrid.js",
                      "~/Content/easyui/extensions/jquery.texteditor.js"));
            // Styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/easyui/themes/icon.css",
                      "~/Content/easyui/themes/color.css",
                      "~/Content/easyui/extensions/texteditor.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

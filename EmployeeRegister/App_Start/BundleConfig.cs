using System.Web;
using System.Web.Optimization;

namespace EmployeeRegister
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jQuery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jQuery/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Modernizr/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts//Bootstrap/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/employeeRegister").Include(
                      "~/Scripts//EmployeeRegister/DetailView.js",
                      "~/Scripts//EmployeeRegister/TreeView.js",
                      "~/Scripts//EmployeeRegister/Particles.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Particles.css",
                      "~/Content/Cards.css",
                      "~/Content/site.css"));
        }
    }
}

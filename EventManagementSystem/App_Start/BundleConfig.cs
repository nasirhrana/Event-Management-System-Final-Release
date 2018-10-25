using System.Web;
using System.Web.Optimization;

namespace EventManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/web/js").Include(
                     "~/web/js/bootstrap.js",
                      "~/web/js/gmaps.js",
                      "~/web/js/jquery2.0.3.min.js",
                      "~/web/js/modernizr.js",
                      "~/web/js/jquery.cookie.js",
                      "~/web/js/screenfull.js",
                     "~/web/js/graph.js"));
            bundles.Add(new StyleBundle("~/web/css").Include(
                      "~/web/css/bootstrap.css",
                      "~/web/css/style.css",
                      "~/web/css///fonts.googleapis.com/css?family=Roboto:400,100,100italic,300,300italic,400italic,500,500italic,700,700italic,900,900italic",
                      "~/web/css/font.css",
                      "~/web/css/font-awesome.css",
                      "~/web/css/site.css"));
        }
    }
}

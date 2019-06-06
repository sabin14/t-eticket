using System.Web;
using System.Web.Optimization;

namespace prabhuEticket
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            // Fonts Custom style
            bundles.Add(new StyleBundle("~/fonts").Include(
                      "~/assets/vendor/fontawesome-free/css/all.min.css",
                      "~/assets/css/google_fonts.css"));
            //Theme Custom styles 
            bundles.Add(new StyleBundle("~/css").Include(
                      "~/assets/css/sb-admin-2.min.css"));
            //bootstrap core javascript
            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                      "~/assets/vendor/jquery/jquery.min.js",
                      "~/assets/vendor/bootstrap/js/bootstrap.bundle.min.js"));
            //core plugin for javascript
            bundles.Add(new ScriptBundle("~/js/core").Include(
                     "~/assets/vendor/jquery-easing/jquery.easing.min.js"));
            //custom scripts for all pages
            bundles.Add(new ScriptBundle("~/js/custom").Include(
                     "~/assets/js/sb-admin-2.min.js"));
            //page level plugins
            bundles.Add(new ScriptBundle("~/js/chart").Include(
                     "~/assets/vendor/chart.js/Chart.min.js"));
            //page level custom scripts
            bundles.Add(new ScriptBundle("~/js/chart/custom").Include(
                    "~/assets/js/demo/chart-area-demo.js",
                    "~/assets/js/demo/chart-pie-demo.js"));
        }
    }
}

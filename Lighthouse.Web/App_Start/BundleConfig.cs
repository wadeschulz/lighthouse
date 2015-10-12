using System.Web;
using System.Web.Optimization;

namespace Lighthouse.Web
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

            // Bundles for jQuery-QueryBuilder
            bundles.Add(new ScriptBundle("~/bundles/qbuilder").Include(
                        "~/Scripts/jQueryBuilder/bower_components/bootstrap-select/dist/js/bootstrap-select.min.js",
                        "~/Scripts/jQueryBuilder/bower_components/bootbox/bootbox.js",
                        "~/Scripts/jQueryBuilder/bower_components/seiyria-bootstrap-slider/dist/bootstrap-slider.min.js",
                        "~/Scripts/jQueryBuilder/bower_components/selectize/dist/js/standalone/selectize.min.js",
                        "~/Scripts/jQueryBuilder/bower_components/jquery-extendext/jQuery.extendext.min.js",
                        "~/Scripts/jQueryBuilder/bower_components/sql-parser/browser/sql-parser.js",
                        "~/Scripts/jQueryBuilder/bower_components/doT/doT.js",
                        "~/Scripts/jQueryBuilder/dist/js/query-builder.js"));

            bundles.Add(new StyleBundle("~/Content/qbuilder").Include(
                        "~/Scripts/jQueryBuilder/bower_components/bootstrap-select/dist/css/bootstrap-select.min.css",
                        "~/Scripts/jQueryBuilder/bower_components/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css",
                        "~/Scripts/jQueryBuilder/bower_components/seiyria-bootstrap-slider/dist/css/bootstrap-slider.min.css",
                        "~/Scripts/jQueryBuilder/bower_components/selectize/dist/css/selectize.bootstrap3.css",
                        "~/Scripts/jQueryBuilder/dist/css/query-builder.default.css",
                        "~/Scripts/jQueryBuilder/dist/css/flags.css"));
        }
    }
}

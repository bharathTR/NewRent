using System.Web;
using System.Web.Optimization;

namespace SampleProject
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            ////////////////////////Datatable
            bundles.Add(new ScriptBundle("~/bundles/DTjs").Include(
                        "~/Scripts/DataTable/jquery.dataTables.min.js"));

            bundles.Add(new StyleBundle("~/Content/DTcss").Include(
                     "~/Content/DataTable/css/jquery.dataTables.min.css"));

            ///////////////////////////////////////////////Dashboard 
            bundles.Add(new StyleBundle("~/bundles/Dashboard").Include(
                       "~/Dashboard Template/assets/css/bootstrap.css",
                        "~/Dashboard Template/assets/css/font-awesome.css",
                        "~/Dashboard Template/assets/css/custom-styles.css",
                         "~/Dashboard Template/assets/js/morris/morris-0.4.3.min.css",
                         "~/Dashboard Template/assets/js/Lightweight-Chart/cssCharts.css",
                         "~/Dashboard Template/assets/materialize/css/materialize.min.css"));




            bundles.Add(new ScriptBundle("~/bundles/DashboardScripts").Include(
                     "~/ Dashboard Template/assets/js/jquery-1.10.2.js",
                     "~/ Dashboard Template/assets/js/bootstrap.min.js",
                     "~/ Dashboard Template/assets/materialize/js/materialize.min.js",
                     "~/ Dashboard Template/assets/js/jquery.metisMenu.js",
                     "~/ Dashboard Template/assets/js/morris/raphael-2.1.0.min.js",
                     "~/ Dashboard Template/assets/js/morris/morris.js",
                     "~/ Dashboard Template/assets/js/easypiechart.js",
                     "~/ Dashboard Template/assets/js/easypiechart-data.js",
                     "~/ Dashboard Template/assets/js/Lightweight-Chart/jquery.chart.js",
                     "~/ Dashboard Template/assets/js/custom-scripts.js"));



            ///////////////////////////////////////////////////////////

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                       "~/Scripts/Login/Validations.js"));

            bundles.Add(new ScriptBundle("~/bundles/Customer").Include(
                      "~/Scripts/Customer/Customer.js",
                      "~/Scripts/Customer/CustomerCreate.js"));

            bundles.Add(new ScriptBundle("~/bundles/Ticket").Include(
                        "~/Scripts/Ticket/Ticket.js"));

            bundles.Add(new StyleBundle("~/bundles/CustomerStyle").Include(
                       "~/Content/Customer/Customer.css"));

            ////////////////////////////////////////////// bootbox///////
            bundles.Add(new ScriptBundle("~/bundles/bootbox").Include(
                        "~/Scripts/bootbox.js"));

        }
    }
}

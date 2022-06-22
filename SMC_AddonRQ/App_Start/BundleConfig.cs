using System.Web;
using System.Web.Optimization;

namespace SMC_AddonRQ
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/plugins/jquery-1.11.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/assets/js/jquery/jquery_ui/jquery-ui.min.js",
                      "~/assets/js/plugins/highcharts/highcharts.js",
                      "~/assets/js/plugins/c3charts/d3.min.js",
                      "~/assets/js/plugins/c3charts/c3.min.js",
                      "~/assets/js/plugins/circles/circles.js",
                      //"~/assets/js/plugins/fullcalendar/lib/moment.min.js",
                      //"~/assets/js/plugins/fullcalendar/fullcalendar.min.js",
                      //"~/assets/allcp/forms/js/jquery-ui-monthpicker.min.js",
                      //"~/assets/allcp/forms/js/jquery-ui-datepicker.min.js",
                      "~/assets/js/plugins/magnific/jquery.magnific-popup.js",
                      "~/assets/js/utility/utility.js",
                      "~/assets/js/demo/demo.js",
                      "~/assets/js/main.js",
                      //"~/assets/js/demo/widgets.js",
                      //"~/assets/js/demo/widgets_sidebar.js",
                      "~/assets/js/pages/dashboard1.js",
                      "~/assets/sweetalert2.js",
                      "~/assets/bootstrap-treeview.js",
                      "~/js/jquery/jquery_ui/jquery_ui.min.js",
                      "~/assets/js/plugins/select2/select2.min.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/assets/fonts/icomoon/icomoon.css",
                      "~/assets/js/plugins/fullcalendar/fullcalendar.min.css",
                      "~/assets/js/plugins/select2/css/core.css",
                      "~/assets/js/plugins/magnific/magnific-popup.css",
                      "~/assets/js/plugins/c3charts/c3.min.css",
                      "~/assets/skin/default_skin/css/theme.css",
                      "~/assets/allcp/forms/css/forms.css",
                      "~/assets/bootstrap-treeview.css",
                      "~/assets/sweetalert2.css",
                      "~/css/jquery.autocomplete.css"
                      ));
        }
    }
}

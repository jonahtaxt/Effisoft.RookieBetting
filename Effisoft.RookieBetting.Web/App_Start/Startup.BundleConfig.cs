using System.Web.Optimization;

namespace Effisoft.RookieBetting.Web
{
    public partial class Startup
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/site/appRookieBetting.js",
                "~/Scripts/site/controllers/homeController.js"));

            bundles.Add(new ScriptBundle("~/bundles/home/index").Include(
                "~/Scripts/site/services/gameService.js",
                "~/Scripts/site/directives/effAvailableSeasons.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/site.css"));
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}

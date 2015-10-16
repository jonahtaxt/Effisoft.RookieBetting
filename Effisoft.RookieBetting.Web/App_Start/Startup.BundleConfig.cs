using System.Web.Optimization;

namespace Effisoft.RookieBetting.Web
{
    public partial class Startup
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular/angular.min.js",
                "~/Scripts/angular/angular-route.min.js",
                "~/Scripts/site/appRookieBetting.js"));

            bundles.Add(new ScriptBundle("~/bundles/home/index").Include(
                "~/Scripts/site/services/gameService.js",
                "~/Scripts/site/services/divisionService.js",
                "~/Scripts/site/services/teamService.js",
                "~/Scripts/site/controllers/homeController.js",
                "~/Scripts/site/controllers/weekGameController.js",
                "~/Scripts/site/controllers/statsController.js",
                "~/Scripts/site/directives/effAvailableSeasons.js",
                "~/Scripts/site/directives/effWeekGameResults.js",
                "~/Scripts/site/directives/effNflDivisions.js"));

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

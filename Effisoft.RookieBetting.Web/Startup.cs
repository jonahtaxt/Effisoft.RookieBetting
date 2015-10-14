using System.Web.Optimization;
using System.Web.Routing;
using Owin;

namespace Effisoft.RookieBetting.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }
    }
}

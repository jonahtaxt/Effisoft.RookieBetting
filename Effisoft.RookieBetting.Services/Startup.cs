using Owin;
using System.Web.Http;

namespace Effisoft.RookieBetting.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            AutofacConfig.RegisterDependencies(app, config);
            app.UseWebApi(config);
        }
    }
}

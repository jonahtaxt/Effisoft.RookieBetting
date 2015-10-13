using Owin;
using System.Web.Http;
using Effisoft.RookieBetting.Security.Helpers;
using Effisoft.RookieBetting.Security.Infrastructure;

namespace Effisoft.RookieBetting.Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ConfigureOAuth(app, config);
            WebApiConfig.Register(config);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            AutofacConfig.RegisterDependencies(app, config);
        }
    }
}

using System;
using System.Web.Http;
using Effisoft.RookieBetting.Security.Helpers;
using Effisoft.RookieBetting.Security.Infrastructure;
using Effisoft.RookieBetting.Services.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Effisoft.RookieBetting.Services
{
    public partial class Startup
    {
        public static Func<ApplicationUserManager> UserManagerFactory { get; private set; }

        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            UserManagerFactory = () =>
            {
                try
                {
                    var userRepository = config.DependencyResolver.GetService(typeof(IUserRepository));
                    var userManager =
                        new ApplicationUserManager(userRepository as IUserRepository);
                    return userManager;
                }
                catch (Exception)
                {
                    return null;
                }
            };

            app.CreatePerOwinContext(UserManagerFactory);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

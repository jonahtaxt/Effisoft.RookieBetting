using System.Threading.Tasks;
using Effisoft.RookieBetting.Security;
using Effisoft.RookieBetting.Security.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Effisoft.RookieBetting.Services.Providers
{
    public class SimpleAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var usuario = await FindUser(context.UserName, context.Password, userManager);
            if (usuario == null)
            {
                context.SetError("user/password_incorrect");
                return;
            }

            if (!usuario.Active)
            {
                context.SetError("Inactive_User");
                return;
            }

            var identity = await userManager.CreateIdentityAsync(usuario, DefaultAuthenticationTypes.ApplicationCookie);
            context.Validated(identity);
            userManager.Dispose();
        }

        private static async Task<User> FindUser(string userName, string password, ApplicationUserManager userManager)
        {
            var user = await userManager.FindAsync(userName, password);
            return user;
        }
    }
}

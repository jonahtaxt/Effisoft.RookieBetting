using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Effisoft.RookieBetting.Security.Helpers
{
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager applicationUserManager,
            IAuthenticationManager authenticationManager)
            : base(applicationUserManager, authenticationManager)
        {
        }
    }
}

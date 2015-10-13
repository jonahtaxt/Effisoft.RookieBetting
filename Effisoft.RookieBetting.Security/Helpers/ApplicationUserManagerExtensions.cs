using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Effisoft.RookieBetting.Security.Helpers
{
    public static class ApplicationUserManagerExtentions
    {
        public static string GetUserCurrrentRole(this IPrincipal user)
        {
            var userIdentity = (ClaimsIdentity)user.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            return role?.Value;
        }
    }
}

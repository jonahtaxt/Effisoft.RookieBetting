using Effisoft.RookieBetting.Security.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security.Helpers
{
    public class ApplicationRoleManager : RoleManager<Role,int>
    {
        public ApplicationRoleManager(IRoleRepository roleRepository)
            : base(roleRepository)
        {
        }
    }
}

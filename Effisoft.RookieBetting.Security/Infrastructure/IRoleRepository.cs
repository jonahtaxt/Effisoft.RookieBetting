using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security.Infrastructure
{
    public interface IRoleRepository : IRoleStore<Role, int>
    {
        List<Role> GetRoles();
    }
}

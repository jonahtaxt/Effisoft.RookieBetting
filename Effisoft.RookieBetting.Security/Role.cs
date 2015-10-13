using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security
{
    public class Role : Common.Models.Security.Role, IRole<int>
    {
        public int Id => RoleId;
    }
}

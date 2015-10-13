using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security.Infrastructure
{
    public interface IUserRepository :
        IUserRoleStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserTwoFactorStore<User, int>
    {
        bool CreateUser(User user);
        List<Role> GetRoles();
        User GetUserById(int userId);
        List<User> GetUsers();
        bool UpdateUser(User user);
    }
}

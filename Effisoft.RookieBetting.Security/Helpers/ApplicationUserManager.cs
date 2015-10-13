using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Effisoft.RookieBetting.Security.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security.Helpers
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserRepository userRepository)
            : base(userRepository)
        {
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            UserLockoutEnabledByDefault = false;

            PasswordHasher = new ApplicationPasswordHasher();
        }

        public async Task<IdentityResult> IsValidUserPasswordAsync(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            return await PasswordValidator.ValidateAsync(password);
        }

        public IdentityResult IsValidUserPassword(string password)
        {
            return IsValidUserPasswordAsync(password).Result;
        }
    }
}

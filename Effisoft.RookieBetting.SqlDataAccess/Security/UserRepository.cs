using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Effisoft.RookieBetting.Infrastructure.Database;
using Effisoft.RookieBetting.Security;
using Effisoft.RookieBetting.Security.Infrastructure;

namespace Effisoft.RookieBetting.SqlDataAccess.Security
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool CreateUser(User user)
        {
            user.CreationDate = DateTime.UtcNow;
            user.UpdateDate = DateTime.UtcNow;
            user.Active = true;

            var success = DatabaseContext.ExecuteProcedure<bool>("AddUser", new
            {
                user.Name,
                user.LastName,
                user.SurName,
                user.Email,
                user.Password,
                user.CreationDate,
                user.UpdateDate,
                user.Active,
                user.RoleId
            });

            return success;
        }

        public List<Role> GetRoles()
        {
            return DatabaseContext.ExecuteProcedure<List<Role>>("GetRoles");
        }

        public User GetUserById(int userId)
        {
            var result = DatabaseContext.ExecuteProcedure<User>("GetUserById",
                new { UserId = userId }, true);

            return result;
        }

        public List<User> GetUsers()
        {
            return DatabaseContext.ExecuteProcedure<List<User>>("GetUsers", true);
        }

        public bool UpdateUser(User user)
        {
            user.UpdateDate = DateTime.UtcNow;
            var success = DatabaseContext.ExecuteProcedure<bool>("UpdateUser", new
            {
                user.UserId,
                user.Name,
                user.LastName,
                user.SurName,
                user.Email,
                user.UpdateDate,
                user.RoleId,
                user.Active
            });

            return success;
        }

        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (user.RoleId <= 0)
                throw new ArgumentNullException("user.RoleId");

            DatabaseContext.ExecuteProcedure<User>("AddUser", new
            {
                user.Name,
                user.LastName,
                user.SurName,
                user.Email,
                user.Password,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow,
                user.Active,
                user.RoleId
            });

            return Task.FromResult(0);
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            var user = DatabaseContext.ExecuteProcedure<User>("GetUserById", new { UserId = userId }, true);

            return Task.FromResult(user);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var user = DatabaseContext.ExecuteProcedure<User>("GetUserByName", new { Email = userName }, true);
            return Task.FromResult(user);
        }

        public Task UpdateAsync(User user)
        {
            DatabaseContext.ExecuteProcedure("UpdateUserPassword", new { user.UserId, user.Password });
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            var roles = DatabaseContext.ExecuteProcedure<List<Role>>("GetRoleByUser", new { UserId = user.Id });

            return Task.FromResult<IList<string>>(roles.Select(r => r.Name).ToList());
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var roles = await GetRolesAsync(user);

            return roles.Contains(roleName);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.Now.AddYears(1));
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(0);
        }
    }
}

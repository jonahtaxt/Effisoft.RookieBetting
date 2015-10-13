using System;
using System.Threading.Tasks;
using System.Web.Http;
using Effisoft.RookieBetting.Security;
using Effisoft.RookieBetting.Security.Helpers;
using Effisoft.RookieBetting.Security.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Services.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationPasswordHasher _applicationPasswordHasher;

        public AccountController(IUserRepository userRepository)
        {
            _applicationUserManager = new ApplicationUserManager(userRepository);
            _applicationPasswordHasher = new ApplicationPasswordHasher();
        }

        [Route("")]
        public string Test()
        {
            return "Caca";
        }

        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.CreationDate = DateTime.Now;
            user.UpdateDate = DateTime.Now;
            user.Password = _applicationPasswordHasher.HashPassword(user.Password);
            user.Active = true;

            var result =
                await _applicationUserManager.CreateAsync(user);

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}

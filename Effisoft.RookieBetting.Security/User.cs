using Microsoft.AspNet.Identity;

namespace Effisoft.RookieBetting.Security
{
    public class User : Common.Models.Security.User, IUser<int>
    {
        public int Id => UserId;

        public string UserName
        {
            get { return Email; }
            set { Email = value; }
        }
    }
}

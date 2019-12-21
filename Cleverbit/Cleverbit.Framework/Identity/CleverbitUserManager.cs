using Cleverbit.Framework.Context;
using Cleverbit.Framework.Helper;

using Microsoft.AspNet.Identity;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cleverbit.Framework.Identity
{
    public class CleverbitUserManager<TUser> : UserManager<TUser, Guid> where TUser : UserIdentityModel
    {
        public CleverbitUserManager(IUserStore<TUser, Guid> store) : base(store)
        {
        }

        public override Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            var task = new Task<bool>(
                () =>
                {
                    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(user.PasswordSalt))
                    {
                        return false;
                    }

                    var hash = CommonHelper.EncodePassword(password, user.PasswordSalt);
                    return hash == user.Password;
                });

            task.Start();
            return task;
        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            return base.CreateIdentityAsync(user, authenticationType);
        }
    }
}

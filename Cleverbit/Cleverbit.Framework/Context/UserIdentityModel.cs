using Microsoft.AspNet.Identity;

using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cleverbit.Framework.Context
{
    public class UserIdentityModel : IUser<Guid>
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public IList<Claim> UserClaims { get; set; }
    }
}

using AutoMapper;

using Cleverbit.Entity.Model;
using Cleverbit.Framework.Context;
using Cleverbit.Framework.Identity;

using System.Collections.Generic;
using System.Security.Claims;

namespace Cleverbit.Infrastructure.Resolvers
{
    public class UserClaimsResolver : IValueResolver<User, UserIdentityModel, IList<Claim>>
    {
        public IList<Claim> Resolve(User source, UserIdentityModel destination, IList<Claim> destMember, ResolutionContext context)
        {
            var claims = new List<Claim> { new Claim(Claims.UserName, source.UserName), new Claim(Claims.UserId, source.Id.ToString()) };
            return claims;
        }
    }
}
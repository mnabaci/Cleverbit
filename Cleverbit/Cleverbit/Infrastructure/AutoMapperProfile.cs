using AutoMapper;

using Cleverbit.Entity.Model;
using Cleverbit.Framework.Context;
using Cleverbit.Infrastructure.Resolvers;

namespace Cleverbit.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserIdentityModel>()
                .ForMember(x => x.UserClaims, y => y.MapFrom<UserClaimsResolver>());
        }
    }
}
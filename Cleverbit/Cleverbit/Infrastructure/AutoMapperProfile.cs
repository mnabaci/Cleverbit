using AutoMapper;

using Cleverbit.Dto.Match;
using Cleverbit.Entity.Model;
using Cleverbit.Framework.Context;
using Cleverbit.Infrastructure.Resolvers;
using Cleverbit.Models;

namespace Cleverbit.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserIdentityModel>()
                .ForMember(x => x.UserClaims, y => y.MapFrom<UserClaimsResolver>());

            this.CreateMap<UserMatchResultDto, UserMatchResultViewModel>();
        }
    }
}
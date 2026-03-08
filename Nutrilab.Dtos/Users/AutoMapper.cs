using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Users
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IUser, UserDto>()
                .ForMember(
                    dest => dest.Roles,
                    opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name))
                );
        }
    }
}

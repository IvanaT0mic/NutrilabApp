using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Roles
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IRole, RoleDto>()
                .ForMember(
                    dest => dest.Permissions,
                    opt => opt.MapFrom(src => src.RolePermissions.Select(ur => ur.Permission.Name))
                );
        }
    }
}

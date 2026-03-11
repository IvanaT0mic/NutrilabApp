using AutoMapper;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.Dtos.Permissions
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<IPermission, PermissionDto>();
        }
    }
}

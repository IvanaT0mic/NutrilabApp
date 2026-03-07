using Nutrilab.DataAccess.Models.RolePermissions;
using Nutrilab.DataAccess.Models.UserRoles;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.Roles
{
    public class Role : IRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        List<IUserRole> IRole.UserRoles => UserRoles.ToList<IUserRole>();

        List<IRolePermission> IRole.RolePermissions => RolePermissions.ToList<IRolePermission>();
    }
}

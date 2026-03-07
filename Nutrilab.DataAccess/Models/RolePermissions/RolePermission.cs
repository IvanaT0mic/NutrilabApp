using Nutrilab.DataAccess.Models.Permissions;
using Nutrilab.DataAccess.Models.Roles;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.RolePermissions
{
    public class RolePermission : IRolePermission
    {
        public int RoleId { get; set; }

        public Role Role { get; set; }

        public int PermissionId { get; set; }

        public Permission Permission { get; set; }

        IRole IRolePermission.Role => Role;

        IPermission IRolePermission.Permission => Permission;
    }
}

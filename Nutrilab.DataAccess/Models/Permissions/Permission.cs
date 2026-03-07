using Nutrilab.DataAccess.Models.RolePermissions;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.Permissions
{
    public class Permission : IPermission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RolePermission> Roles { get; set; }

        List<IRolePermission> IPermission.Roles => Roles.ToList<IRolePermission>();
    }
}

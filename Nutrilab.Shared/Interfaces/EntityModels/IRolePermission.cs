namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IRolePermission
    {
        int RoleId { get; }

        IRole Role { get; }

        int PermissionId { get; }

        IPermission Permission { get; }
    }
}

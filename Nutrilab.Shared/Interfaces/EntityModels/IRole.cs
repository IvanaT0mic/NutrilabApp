namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IRole
    {
        int Id { get; }

        string Name { get; }

        List<IUserRole> UserRoles { get; }

        List<IRolePermission> RolePermissions { get; }
    }
}

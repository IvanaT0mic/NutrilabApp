namespace Nutrilab.Dtos.Roles
{
    public record UpdateRolePermissionsDto
    {
        public List<int> PermissionIds { get; init; }
    }
}

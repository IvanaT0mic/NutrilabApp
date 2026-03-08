namespace Nutrilab.Dtos.Users
{
    public record UpdateUserRolesDto
    {
        public List<int> RoleIds { get; init; }
    }
}

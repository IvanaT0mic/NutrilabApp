namespace Nutrilab.Dtos.Roles
{
    public sealed class RoleOutgoingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}

namespace Nutrilab.Shared.Models
{
    public class UserPrincipal
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}

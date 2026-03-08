namespace Nutrilab.Shared.Models
{
    public interface IUserPrincipal
    {
        long Id { get; }
        string Email { get; }
        List<string> Roles { get; }
        List<string> Permissions { get; }
    }

    public class UserPrincipal : IUserPrincipal
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}

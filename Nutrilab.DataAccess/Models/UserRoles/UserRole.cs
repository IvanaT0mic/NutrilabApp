using Nutrilab.DataAccess.Models.Roles;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityModels;

namespace Nutrilab.DataAccess.Models.UserRoles
{
    public class UserRole : IUserRole
    {
        public long UserId { get; set; }

        public User User { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        IUser IUserRole.User => User;

        IRole IUserRole.Role => Role;
    }
}

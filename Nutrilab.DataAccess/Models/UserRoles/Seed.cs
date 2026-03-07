namespace Nutrilab.DataAccess.Models.UserRoles
{
    public static class Seed
    {
        public static UserRole[] Data => new[]
        {
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 2, RoleId = 2 },
            new UserRole { UserId = 3, RoleId = 3 },
            new UserRole { UserId = 4, RoleId = 4 }
        };
    }
}

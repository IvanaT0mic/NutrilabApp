namespace Nutrilab.DataAccess.Models.Roles
{
    public static class Seed
    {
        public static Role[] Data => new[]
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Maintainer" },
            new Role { Id = 3, Name = "Editor" },
            new Role { Id = 4, Name = "User" }
        };
    }
}

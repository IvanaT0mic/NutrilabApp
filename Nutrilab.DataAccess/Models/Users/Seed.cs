namespace Nutrilab.DataAccess.Models.Users
{
    public static class Seed
    {


        public static User[] Data => new[]
        {
            new User
            {
                Id = 1,
                Email = "admin@nutriplan.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 2,
                Email = "maintainer@nutriplan.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test!123"),
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 3,
                Email = "editor@nutriplan.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test!123"),
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 4,
                Email = "user@nutriplan.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test!123"),
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };
    }
}

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
                PasswordHash = "$2a$11$3FxdA3t4oyB3FO.CNmY.Tem4sy3AI.//yOaJO/suHNahlRyysGE3a",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 2,
                Email = "maintainer@nutriplan.com",
                PasswordHash = "$2a$11$t58KV3nhGBV.iCCVlDo7cekNrrlpziyB5YX8GE5ZxBZg1yC3PvB8m",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 3,
                Email = "editor@nutriplan.com",
                PasswordHash = "$2a$11$t58KV3nhGBV.iCCVlDo7cekNrrlpziyB5YX8GE5ZxBZg1yC3PvB8m",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new User {
                Id = 4,
                Email = "user@nutriplan.com",
                PasswordHash = "$2a$11$t58KV3nhGBV.iCCVlDo7cekNrrlpziyB5YX8GE5ZxBZg1yC3PvB8m",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };
    }
}

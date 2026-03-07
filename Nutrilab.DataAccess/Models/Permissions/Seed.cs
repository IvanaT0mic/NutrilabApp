namespace Nutrilab.DataAccess.Models.Permissions
{
    namespace Nutrilab.DataAccess.Models.Permissions
    {
        public static class Seed
        {
            public static Permission[] Data => new[]
            {
            new Permission { Id = 1, Name = "user.manage.roles" },
            new Permission { Id = 2, Name = "recipe.create" },
            new Permission { Id = 3, Name = "recipe.edit" },
            new Permission { Id = 4, Name = "recipe.delete" },
            new Permission { Id = 5, Name = "recipe.read" },
            new Permission { Id = 6, Name = "recipe.favourite" }
        };
        }
    }
}

namespace Nutrilab.DataAccess.Models.RolePermissions
{
    public static class Seed
    {
        public static RolePermission[] Data => new[]
        {
            // Admin - sve
            new RolePermission { RoleId = 1, PermissionId = 1 },
            new RolePermission { RoleId = 1, PermissionId = 2 },
            new RolePermission { RoleId = 1, PermissionId = 3 },
            new RolePermission { RoleId = 1, PermissionId = 4 },
            new RolePermission { RoleId = 1, PermissionId = 5 },
            new RolePermission { RoleId = 1, PermissionId = 6 },
            // Maintainer
            new RolePermission { RoleId = 2, PermissionId = 1 },
            // Editor
            new RolePermission { RoleId = 3, PermissionId = 2 },
            new RolePermission { RoleId = 3, PermissionId = 3 },
            new RolePermission { RoleId = 3, PermissionId = 4 },
            new RolePermission { RoleId = 3, PermissionId = 5 },
            // User
            new RolePermission { RoleId = 4, PermissionId = 5 },
            new RolePermission { RoleId = 4, PermissionId = 6 }
        };
    }
}

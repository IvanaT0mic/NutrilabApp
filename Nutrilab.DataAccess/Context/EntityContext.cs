using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Models.FavouriteRecipes;
using Nutrilab.DataAccess.Models.Ingredients;
using Nutrilab.DataAccess.Models.Permissions;
using Nutrilab.DataAccess.Models.RecipeIngredients;
using Nutrilab.DataAccess.Models.Recipes;
using Nutrilab.DataAccess.Models.RolePermissions;
using Nutrilab.DataAccess.Models.Roles;
using Nutrilab.DataAccess.Models.UserRoles;
using Nutrilab.DataAccess.Models.Users;
using Nutrilab.Shared.Interfaces.EntityAudit;

namespace Nutrilab.DataAccess.Context
{
    public sealed class EntityContext(DbContextOptions options) : DbContext(options)
    {
        #region Db sets
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<FavouriteRecipe> FavouriteRecipes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IHasCreatedAt>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
                entry.Entity.CreatedAt = DateTime.UtcNow;

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

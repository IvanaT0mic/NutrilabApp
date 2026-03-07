using Microsoft.AspNetCore.Http;
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

namespace Nutrilab.DataAccess.Context
{
    public sealed class EntityContext : DbContext
    {
        readonly IHttpContextAccessor _httpContextAccessor;

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

        public EntityContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityContext).Assembly);
        }
    }
}

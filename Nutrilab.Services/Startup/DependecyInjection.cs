using Microsoft.Extensions.DependencyInjection;
using Nutrilab.Repositories;
using Nutrilab.Repositories.Startup;
using Nutrilab.Services.Handlers;

namespace Nutrilab.Services.Startup
{
    public static class DependecyInjection
    {
        public static void ConfigureRepositories(
            this IServiceCollection services,
            string connectionString
        )
        {
            //register handlers
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            //register repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IFavouriteRecipeRepository, FavouriteRecipeRepository>();

            services.ConfigureContext(connectionString);
        }

        public static async Task AutoUpdateDatabaseContextAsync(this IServiceProvider app)
        {
            await app.AutoMigrateAsync();
        }
    }
}

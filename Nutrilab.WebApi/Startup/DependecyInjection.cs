using Nutrilab.Services;
using Nutrilab.Services.AuthServices;
using Nutrilab.Services.Startup;

namespace Nutrilab.WebApi.Startup
{
    public static class DependecyInjection
    {
        public static void ConfigureServices(
            this IServiceCollection services,
            string connectionString
        )
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.ConfigureRepositories(connectionString);
        }
    }
}

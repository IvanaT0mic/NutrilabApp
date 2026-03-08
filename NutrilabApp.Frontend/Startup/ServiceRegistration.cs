using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.RecipeServices;
using NutrilabApp.Frontend.Services.RoleServices;
using NutrilabApp.Frontend.Services.UserServices;

namespace NutrilabApp.Frontend.Startup
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<TokenService>();
            services.AddScoped<AuthMessageHandler>();

            services.AddScoped<UserApiService>();
            services.AddScoped<RoleApiService>();
            services.AddScoped<RecipeApiService>();
        }
    }
}

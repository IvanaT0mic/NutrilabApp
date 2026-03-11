using NutrilabApp.Frontend.Services;
using NutrilabApp.Frontend.Services.Interceptors;
using NutrilabApp.Frontend.Services.Interceptors.ErrorHandlers;
using NutrilabApp.Frontend.Services.NotificationServices;
using NutrilabApp.Frontend.Services.RecipeServices;

namespace NutrilabApp.Frontend.Startup
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<NotificationService>();

            services.AddScoped<ConnectivityService>();
            services.AddScoped<PageActionGuard>();
            services.AddScoped<AuthMessageHandler>();
            services.AddScoped<ErrorInterceptor>();

            services.AddScoped<AuthService>();
            services.AddScoped<TokenService>();
            services.AddScoped<UserApiService>();
            services.AddScoped<RoleApiService>();
            services.AddScoped<RecipeApiService>();
            services.AddScoped<ShoppingListApiService>();
            services.AddScoped<IngredientApiService>();
        }
    }
}

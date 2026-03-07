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

            services.ConfigureRepositories(connectionString);
        }
    }
}

namespace Nutrilab.WebApi.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowPWA", policy =>
                {
                    policy.WithOrigins(
                            "http://localhost:5043",
                            "https://localhost:7206"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }
    }
}

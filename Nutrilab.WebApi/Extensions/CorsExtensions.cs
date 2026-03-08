namespace Nutrilab.WebApi.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://localhost:7206")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
    }
}

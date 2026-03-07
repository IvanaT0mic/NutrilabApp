namespace Nutrilab.WebApi.Extensions
{
    public static class SettingsExtensions
    {
        public static string ConfigureConnectionString(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetSection("ConnectionString");

            return connectionString?.Value ?? String.Empty;
        }
    }
}

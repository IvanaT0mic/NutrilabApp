using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nutrilab.DataAccess.Context;

namespace Nutrilab.Repositories.Startup
{
    public static class DependecyInjection
    {
        public static void ConfigureContext(this IServiceCollection services, string connectionString)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static async Task AutoMigrateAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<EntityContext>();
            var pending = await db.Database.GetPendingMigrationsAsync();
            if (pending.Any())
                await db.Database.MigrateAsync();
        }
    }
}

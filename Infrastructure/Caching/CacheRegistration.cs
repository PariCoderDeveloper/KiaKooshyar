using KiaKooshar.Infrastructure.Caching.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace KiaKooshar.Infrastructure.Caching
{
    public class CacheRegistration
    {
        public static async Task CacheSeeder (
            IServiceProvider service,
            CancellationToken cancellationToken = default
            )
        {
            using var scope = service.CreateScope ();
            var userCacheSeeder = scope.ServiceProvider
                .GetRequiredService<UserCacheSeeder> ();
            await userCacheSeeder.SeedAsync (cancellationToken);
        }
    }
}

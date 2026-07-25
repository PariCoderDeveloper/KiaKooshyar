using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Infrastructure.Persistence.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace KiaKooshar.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices (
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddMemoryCache ();

            services.AddSingleton<IConnectionMultiplexer> (_ =>
            {
                return ConnectionMultiplexer.Connect (
                    configuration.GetConnectionString ("Redis")
                    );
            });

            services.AddScoped<ICacheService, HybridCacheService> ();
            services.AddScoped<ILocalCacheService, MemoryCacheService> ();
            services.AddScoped<IDistributedCacheService, RedisCacheService> ();

            return services;
        }
    }
}

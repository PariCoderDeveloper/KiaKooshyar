using KiaKooshar.Application.Cachings;
using KiaKooshar.Infrastructure.Caching;
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

            return services;
        }
    }
}

using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Infrastructure.Caching.Options;
using KiaKooshar.Infrastructure.Caching.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace KiaKooshar.Infrastructure.Caching.Extensions
{
    public static class CacheServiceExtensions
    {
        public static IServiceCollection AddCacheService (
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var cacheSettings = configuration
                .GetSection (CacheSettings.SectionName)
                .Get<CacheSettings> ()
                ?? new CacheSettings { Provider = "Memory" };
            services.Configure<CacheSettings> (
                configuration.GetSection (CacheSettings.SectionName)
                );
            switch ( cacheSettings.Provider?.ToLowerInvariant () )
            {
                case "redis":
                    services.AddSingleton<IConnectionMultiplexer> (sp =>
                    {
                        var options = ConfigurationOptions.Parse (
                            configuration.GetConnectionString ("Redis")
                        );
                        options.AbortOnConnectFail = false;
                        return ConnectionMultiplexer.Connect (options);
                    });
                    services.AddScoped<ICacheService, RedisCacheService> ();
                    break;
                case "memory":
                    services.AddMemoryCache ();
                    services.AddScoped<ICacheService, MemoryCacheService> ();
                    break;
                default:
                    throw new InvalidOperationException (
                        $"Cache provider '{cacheSettings.Provider}' is not supported.");
            }
            return services;
        }
    }
}

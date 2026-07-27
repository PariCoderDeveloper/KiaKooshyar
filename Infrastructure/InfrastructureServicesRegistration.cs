using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Infrastructure.Persistence.Authentication.Jwt;
using KiaKooshar.Infrastructure.Persistence.Caching.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

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
            services.AddScoped<IJwtProvider, JwtProvider> ();
            services
            .AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer (options =>
            {
                options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey (
                            Encoding.UTF8.GetBytes (
                                configuration["Jwt:Key"]!
                        ))
                };
            });
            return services;
        }
    }
}

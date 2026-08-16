using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using KiaKooshar.Application.Features.Interfaces.Files;
using KiaKooshar.Application.Features.Interfaces.HttpContext;
using KiaKooshar.Infrastructure.Caching.Extensions;
using KiaKooshar.Infrastructure.Files;
using KiaKooshar.Infrastructure.Identities.RateLimiting;
using KiaKooshar.Infrastructure.Persistence;
using KiaKooshar.Infrastructure.Persistence.Authentication.Jwt;
using KiaKooshar.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace KiaKooshar.Infrastructure.DependencyInjection
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices (
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddCacheService (
                configuration
            );
            services.AddMemoryCache ();
            services.AddSingleton<IConnectionMultiplexer> (sp =>
            {
                var options = ConfigurationOptions.Parse (
                    configuration.GetConnectionString ("Redis")
                );
                options.AbortOnConnectFail = false;
                return ConnectionMultiplexer.Connect (options);
            });
            #region FileCoonverter
            services.AddSingleton<IFileConverter, FileConverter> ();
            #endregion
            #region HealthCheck
            services.AddHealthChecks ()
                 .AddDbContextCheck<DatabaseContext> (
                    name: "database",
                    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                    tags: new[] { "db", "sql" }
                ).AddSqlServer (
                    connectionString: configuration.GetConnectionString ("DefaultConnection")!,
                    name: "sqlserver",
                    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                    tags: new[] { "db", "sql" }
                ).AddRedis (
                    redisConnectionString: configuration.GetConnectionString ("Redis")!,
                    name: "redis",
                    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy,
                    tags: new[] { "cache", "redis" }
                );
            #endregion
            #region 
            services.AddHealthChecksUI (setup =>
            {
                setup.SetEvaluationTimeInSeconds (15);
                setup.AddHealthCheckEndpoint ("Kiakooshyar API", "/health");
            }).AddInMemoryStorage ();
            #endregion
            services.AddScoped<IJwtProvider, JwtProvider> ();
            services.AddScoped<IRequestContext, HttpRequestContext> ();
            services.AddScoped<ICurrentUserService, CurrentUserService> ();
            services.AddRepositories ();

            AddRateLimit.AddFixedWindowRateLimit (services, configuration);

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
            services.Configure<JwtSettings> (
                configuration.GetSection ("Jwt")
            );

            return services;
        }
    }
}

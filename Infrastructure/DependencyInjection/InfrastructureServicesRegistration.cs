using Hangfire;
using Hangfire.SqlServer;
using KiaKooshar.Application.Features.Construct.JWT;
using KiaKooshar.Application.Features.Interfaces.Cache;
using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using KiaKooshar.Application.Features.Interfaces.Files;
using KiaKooshar.Application.Features.Interfaces.HttpContext;
using KiaKooshar.Application.Features.Interfaces.Jobs;
using KiaKooshar.Application.Features.Interfaces.SignalR;
using KiaKooshar.Application.Features.Jobs;
using KiaKooshar.Infrastructure.BackgroundJobs;
using KiaKooshar.Infrastructure.Caching.Extensions;
using KiaKooshar.Infrastructure.Caching.Seed;
using KiaKooshar.Infrastructure.Files;
using KiaKooshar.Infrastructure.Persistence;
using KiaKooshar.Infrastructure.Persistence.Authentication.Jwt;
using KiaKooshar.Infrastructure.RateLimiting;
using KiaKooshar.Infrastructure.Services;
using KiaKooshar.Infrastructure.SignalRHub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
            #region HealthChecksUI
            services.AddHealthChecksUI (setup =>
            {
                setup.SetEvaluationTimeInSeconds (15);
                setup.AddHealthCheckEndpoint ("Kiakooshyar API", "/health");
            }).AddInMemoryStorage ();
            #endregion
            #region Hangfire
            services.AddHangfire (config => config
                .SetDataCompatibilityLevel (CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer ()
                .UseRecommendedSerializerSettings ()
                .UseSqlServerStorage (
                    configuration.GetConnectionString ("DefaultConnection"),
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes (5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes (5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true
                    })
            );
            services.AddHangfireServer ();
            services.AddScoped<IBackgroundJobScheduler, HangfireJobScheduler> ();
            services.AddScoped<RefreshTokenCleanupJob> ();
            #endregion
            #region CacheSeeder
            services.AddScoped<IUserCacheSeeder, UserCacheSeeder> ();
            #endregion

            #region signalR
            services.AddSignalR ();
            services.AddSingleton<IUserIdProvider, CustomUserIdProvider> ();
            services.AddScoped<IUserNotificationService, UserNotificationService> ();
            #endregion
            services.AddScoped<IJwtProvider, JwtProvider> ();
            services.AddScoped<IRequestContext, HttpRequestContext> ();
            services.AddScoped<ICurrentUserService, CurrentUserService> ();
            services.AddRepositories ();

            AddRateLimit.AddFixedWindowRateLimit (services, configuration);
            #region AddAuthentication
            services.AddAuthentication (
                JwtBearerDefaults.AuthenticationScheme
                )
                .AddJwtBearer (options =>
                {
                    options.MapInboundClaims = false;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                configuration["Jwt:Issuer"],

                            ValidAudience =
                                configuration["Jwt:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey (
                                    Encoding.UTF8.GetBytes (
                                        configuration["Jwt:Key"]!
                                    )
                                ),

                            ClockSkew = TimeSpan.Zero
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken =
                                context.Request.Cookies["access-token"];

                            if ( !string.IsNullOrEmpty (accessToken) )
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context =>
                        {
                            switch ( context.Exception )
                            {
                                case SecurityTokenExpiredException:
                                    context.Response.Headers["X-Token-Expired"] =
                                        "true";
                                    break;

                                case SecurityTokenInvalidSignatureException:
                                    context.Response.Headers["X-Token-Invalid"] =
                                        "true";
                                    break;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            services.Configure<JwtSettings> (
                configuration.GetSection ("Jwt")
            );
            #endregion
            return services;
        }
    }
}

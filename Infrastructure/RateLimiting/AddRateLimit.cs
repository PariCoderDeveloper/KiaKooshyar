using KiaKooshar.Domain.BussinessEnums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.RateLimiting;

namespace KiaKooshar.Infrastructure.RateLimiting
{
    public static class AddRateLimit
    {
        public static IServiceCollection AddFixedWindowRateLimit (
            this IServiceCollection services,
            IConfiguration configuration
            )
        {
            var rules = configuration
                .GetSection ("RateLimiting:Rules")
                .Get<List<FixedWindowRateLimitOptions>> ();
            services.AddRateLimiter (options =>
            {
                var globalRule = rules.FirstOrDefault (
                     r => r.Scope == RateLimitScope.Global
                    );
                if ( globalRule is not null )
                    options.GlobalLimiter = CreateLimiter (globalRule);
                foreach ( var rule in rules.Where (
                    r => r.Scope == RateLimitScope.Private
                    ) )
                {
                    var policyName = rule.Name;
                    options.AddPolicy (policyName, httpContext =>
                    {
                        return RateLimitPartition.GetFixedWindowLimiter (
                            partitionKey: GetPartitionKey (httpContext, rule),
                            factory: _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = rule.PermitLimit,
                                Window = rule.Window,
                                QueueLimit = rule.QueueLimit,
                                QueueProcessingOrder = rule.QueueProcessingOrder
                            });
                    });
                }
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
            return services;
        }
        private static PartitionedRateLimiter<HttpContext> CreateLimiter (
            FixedWindowRateLimitOptions rule
            )
        {
            return PartitionedRateLimiter.Create<HttpContext, string> (
                httpContext =>
            {
                var key = httpContext.Connection.RemoteIpAddress?
                    .ToString () ?? "unknown";
                return RateLimitPartition.GetFixedWindowLimiter (
                    key,
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = rule.PermitLimit,
                        Window = rule.Window,
                        QueueLimit = rule.QueueLimit,
                        QueueProcessingOrder = rule.QueueProcessingOrder
                    });
            });
        }
        private static string GetPartitionKey (
            HttpContext context,
            FixedWindowRateLimitOptions rule
            )
        {
            var ip = context.Connection.RemoteIpAddress?
                .ToString () ?? "unknown";
            return $"{ip}:{rule.Name}";
        }

    }
}

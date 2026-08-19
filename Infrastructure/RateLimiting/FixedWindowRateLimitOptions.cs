using KiaKooshar.Domain.BussinessEnums;
using System.Threading.RateLimiting;

namespace KiaKooshar.Infrastructure.RateLimiting
{
    public sealed class RateLimitOptions
    {
        public FixedWindowRateLimitOptions? RefreshToken { get; set; }
        public FixedWindowRateLimitOptions? Controller { get; set; }
    }
    public sealed class FixedWindowRateLimitOptions
    {
        public required string Name { get; set; }
        public RateLimitScope Scope { get; set; }
        public int PermitLimit { get; set; }
        public TimeSpan Window { get; set; }
        public int QueueLimit { get; set; }
        public QueueProcessingOrder QueueProcessingOrder { get; set; }
    }
}

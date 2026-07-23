using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ICachable
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        string? CacheGroup { get; set; }
        CacheExpiration Expiration { get; set; }
    }
}
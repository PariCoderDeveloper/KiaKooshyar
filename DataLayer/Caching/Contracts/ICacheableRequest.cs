using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ICacheableRequest
    {
        bool BypassCache { get; }
        string CacheKey { get; }
        string? CacheGroup { get; }
        CacheExpiration Expiration { get; }
    }
}
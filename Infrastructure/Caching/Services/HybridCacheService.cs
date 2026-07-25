using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Infrastructure.Caching.Services
{
    public class HybridCacheService : ICacheService
    {
        private readonly ILocalCacheService _memoryCache;
        private readonly IDistributedCacheService _redisCache;
        public HybridCacheService (
            ILocalCacheService memoryCache,
            IDistributedCacheService redisCache
            )
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        public async Task ClearAsync ()
        {
            _memoryCache.Clear ();
            await _redisCache.ClearAsync ();
        }

        public async Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            var memoryValue = _memoryCache.Get<T> (key);
            if ( memoryValue is not null )
                return memoryValue;
            var redisValue = await _redisCache.GetAsync<T> (
                key,
                cancellationToken
                );
            if ( redisValue is null )
                return default;
            _memoryCache.Set<T> (
                key,
                redisValue,
                CachePolicy.Medium
                );
            return redisValue;
        }

        public async Task RemoveAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            _memoryCache.Remove (key);
            await _redisCache.RemoveAsync (
                key,
                cancellationToken
            );
        }

        public async Task RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken = default
            )
        {
            await _redisCache.RemoveByPrefixAsync (
                prefix,
                cancellationToken
            );
        }
        public async Task SetAsync<T> (
            string key,
            T value,
            CacheExpiration expiration,
            CancellationToken cancellationToken = default
            )
        {
            _memoryCache.Set (
                key,
                value,
                expiration
            );
            await _redisCache.SetAsync (
                key,
                value,
                expiration,
                cancellationToken
            );
        }
    }
}

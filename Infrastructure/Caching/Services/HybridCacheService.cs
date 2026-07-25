using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Infrastructure.Caching.Services
{
    public class HybridCacheService : ICacheService
    {
        private readonly MemoryCacheService _memoryCache;
        private readonly RedisCacheService _redisCache;
        public HybridCacheService (
            MemoryCacheService memoryCache,
            RedisCacheService redisCache
            )
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
        }

        public async Task ClearAsync ()
        {
            await _memoryCache.ClearAsync ();
            await _redisCache.ClearAsync ();
        }

        public Task<bool> ExistAsync ( string key, CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException ();
        }

        public async Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            var memoryValue = await _memoryCache.GetAsync<T> (
                key,
                cancellationToken
                );
            if ( memoryValue is not null )
                return memoryValue;
            var redisValue = await _redisCache.GetAsync<T> (
                key,
                cancellationToken
                );
            if ( redisValue is null )
                return default;
            await _memoryCache.SetAsync (
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
            await _memoryCache.RemoveAsync (
                key,
                cancellationToken
            );
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
            await _memoryCache.RemoveByPrefixAsync (
                prefix,
                cancellationToken
            );
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
            await _memoryCache.SetAsync (
                key,
                value,
                expiration,
                cancellationToken
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

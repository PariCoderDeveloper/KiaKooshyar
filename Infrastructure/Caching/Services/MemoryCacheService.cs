using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace KiaKooshar.Infrastructure.Caching.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, byte> _keys = new ();
        public MemoryCacheService (
            IMemoryCache memoryCache
            )
        {
            _memoryCache = memoryCache;
        }

        public Task ClearAsync ()
        {
            foreach ( var key in _keys.Keys )
            {
                _memoryCache.Remove (key);
            }
            _keys.Clear ();
            return Task.CompletedTask;
        }

        public Task<bool> ExistAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            bool exist = _memoryCache.TryGetValue (key, out _);
            return Task.FromResult (exist);
        }

        public Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();

            _memoryCache.TryGetValue (key, out T? value);
            return Task.FromResult (value);
        }

        public Task RemoveAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();

            _memoryCache?.Remove (key);
            _keys.TryRemove (key, out _);
            return Task.CompletedTask;
        }

        public Task RemoveByPrefixAsync ( string prefix, CancellationToken cancellationToken = default )
        {
            throw new NotImplementedException ();
        }
        public Task SetAsync<T> (
            string key,
            T value,
            CacheExpiration expiration,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();

            _memoryCache.Set (
                key,
                value,
                expiration.AbsoluteExpiration
                );
            _keys.TryAdd (key, 0);
            return Task.CompletedTask;
        }
    }
}

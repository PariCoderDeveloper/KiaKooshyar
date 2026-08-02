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
        public bool Exist (
            string key
            )
        {
            bool exist = _memoryCache.TryGetValue (key, out _);
            return exist;
        }
        Task ICacheService.ClearAsync ()
        {
            foreach ( var key in _keys.Keys )
                _memoryCache.Remove (key);
            _keys.Clear ();
            return Task.CompletedTask;
        }

        Task<T?> ICacheService.GetAsync<T> (
            string key,
            CancellationToken cancellationToken
            ) where T : default
        {
            cancellationToken.ThrowIfCancellationRequested ();
            _memoryCache.TryGetValue (key, out T? value);
            return Task.FromResult (value);
        }

        Task ICacheService.RemoveAsync (
            string key,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            _memoryCache?.Remove (key);
            _keys.TryRemove (key, out _);
            return Task.CompletedTask;
        }

        Task ICacheService.RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            foreach ( var key in _keys.Keys )
                if ( key.StartsWith (prefix) )
                    _memoryCache.Remove (key);
            return Task.CompletedTask;
        }

        Task ICacheService.SetAsync<T> (
            string key,
            T value,
            CacheExpiration expiration,
            CancellationToken cancellationToken
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

using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;

namespace KiaKooshar.Infrastructure.Caching.Services
{
    public class MemoryCacheService : ILocalCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ConcurrentDictionary<string, byte> _keys = new ();
        public MemoryCacheService (
            IMemoryCache memoryCache
            )
        {
            _memoryCache = memoryCache;
        }

        public void Clear ()
        {
            foreach ( var key in _keys.Keys )
                _memoryCache.Remove (key);
            _keys.Clear ();
        }

        public bool Exist (
            string key
            )
        {
            bool exist = _memoryCache.TryGetValue (key, out _);
            return exist;
        }

        public T? Get<T> ( string key )
        {
            _memoryCache.TryGetValue (key, out T? value);
            return value;
        }
        public void Remove ( string key )
        {
            _memoryCache?.Remove (key);
            _keys.TryRemove (key, out _);
        }
        public void Set<T> (
            string key,
            T value,
            CacheExpiration expiration
            )
        {
            _memoryCache.Set (
                key,
                value,
                expiration.AbsoluteExpiration
                );
            _keys.TryAdd (key, 0);
        }
    }
}

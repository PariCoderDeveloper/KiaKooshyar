using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using Microsoft.Extensions.Caching.Memory;

namespace KiaKooshar.Infrastructure.Caching.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService (
            IMemoryCache memoryCache
            )
        {
            _memoryCache = memoryCache;
        }

        public Task ClearAsync ()
        {
            _memoryCache.
        }

        public Task ExistAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            throw new NotImplementedException ();
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

        public Task RemoveAasyc (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();

            _memoryCache?.Remove (key);
            return Task.CompletedTask;
        }

        //public Task RemoveByPrefixAsync (
        //    string prefix,
        //    CancellationToken cancellationToken = default
        //    )
        //{
        //    cancellationToken.ThrowIfCancellationRequested ();
        //    foreach ( var endpoint in _memoryCache.GetEndPoints () )
        //    {
        //        var server = _memoryCache.GetServer (endpoint);
        //        foreach ( var key in server.Keys (pattern: $"{prefix}") )
        //        {
        //            await _memoryCache.KeyDeleteAsync (key);
        //        }
        //    }
        //}

        public Task RemoveGroupAsync (
            string key,
            CancellationToken cancellationToken = default
            )
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
            return Task.CompletedTask;
        }
    }
}

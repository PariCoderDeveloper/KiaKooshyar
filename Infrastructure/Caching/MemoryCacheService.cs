using KiaKooshar.Application.Cachings;
using Microsoft.Extensions.Caching.Memory;

namespace KiaKooshar.Infrastructure.Caching
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
            throw new NotImplementedException ();
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

        public Task RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken = default
            )
        {
            throw new NotImplementedException ();
        }

        public Task RemoveGroupAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            throw new NotImplementedException ();
        }

        public Task<T> SetAsync<T> (
            string key,
            T value,
            TimeSpan expiration,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();

            _memoryCache.Set (key, value, expiration);
            return Task.FromResult (value);
        }
    }
}

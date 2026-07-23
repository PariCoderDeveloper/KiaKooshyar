using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            );
        Task SetAsync<T> (
            string key,
            T value,
            CacheExpiration expiration,
            CancellationToken cancellationToken = default
            );
        Task RemoveAasyc (
            string key,
            CancellationToken cancellationToken = default
            );
        Task RemoveGroupAsync (
            string key,
            CancellationToken cancellationToken = default
            );
        Task RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken = default
            );
        Task ClearAsync ();
        Task<bool> ExistAsync (
            string key,
            CancellationToken cancellationToken = default
            );
    }
}

using KiaKooshar.Application.Caching.Policies;

namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ILocalCacheService
    {
        T? Get<T> ( string key );
        void Set<T> (
            string key,
            T value,
            CacheExpiration expiration
        );
        void Remove ( string key );
        bool Exist ( string key );
        void Clear ();
    }
}

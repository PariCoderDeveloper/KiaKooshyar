using KiaKooshar.Application.Cachings;
using StackExchange.Redis;

namespace KiaKooshar.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _connection;
        private readonly IDatabase _database;
        public RedisCacheService (
            IConnectionMultiplexer connection
            )
        {
            _connection = connection;
            _database = connection.GetDatabase ();
        }
        public Task ClearAsync ()
        {
            throw new NotImplementedException ();
        }

        public Task<bool> ExistAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            bool exist = _database.KeyExists (key);
            return exist;
        }

        public Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            throw new NotImplementedException ();
        }

        public Task RemoveAasyc (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            throw new NotImplementedException ();
        }

        public Task RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            throw new NotImplementedException ();
        }

        public Task RemoveGroupAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
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
        }
    }
}

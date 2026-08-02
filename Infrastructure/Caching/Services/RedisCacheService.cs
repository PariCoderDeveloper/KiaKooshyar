using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using StackExchange.Redis;
using System.Text.Json;

namespace KiaKooshar.Infrastructure.Caching.Services
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
        public async Task ClearAsync ()
        {
            foreach ( var endpoint in _connection.GetEndPoints () )
            {
                var server = _connection.GetServer (endpoint);
                await server.FlushAllDatabasesAsync ();
            }
        }
        public async Task<T?> GetAsync<T> (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            RedisValue value = await _database.StringGetAsync (key);
            if ( value.IsNullOrEmpty )
                return default;
            return JsonSerializer.Deserialize<T> (value!);
        }
        public async Task RemoveAsync (
            string key,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            await _database.KeyDeleteAsync (key);
        }
        public async Task RemoveByPrefixAsync (
            string prefix,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            foreach ( var endpoint in _connection.GetEndPoints () )
            {
                var server = _connection.GetServer (endpoint);
                foreach ( var key in server.Keys (pattern: $"{prefix}") )
                {
                    await _database.KeyDeleteAsync (key);
                }
            }
        }
        public async Task SetAsync<T> (
            string key,
            T value,
            CacheExpiration expiration,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var json = JsonSerializer.Serialize (value);
            await _database.StringSetAsync (
                key,
                json,
                expiration.AbsoluteExpiration
                );
        }
    }
}

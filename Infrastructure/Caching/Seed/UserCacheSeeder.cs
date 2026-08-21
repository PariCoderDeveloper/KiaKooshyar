using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.DTOs.Identities.Cache;
using KiaKooshar.Infrastructure.Caching.Services;
using KiaKooshar.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Caching.Seed
{
    public class UserCacheSeeder
    {
        private readonly DatabaseContext _context;
        private readonly RedisCacheService _cacheService;
        public UserCacheSeeder (
            DatabaseContext context,
            RedisCacheService cacheService
            )
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task SeedAsync (
            CancellationToken cancellationToken = default
            )
        {
            var users = await _context.Users
                .AsNoTracking ()
                .Select (u => new CachedUserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName
                }).ToListAsync (cancellationToken);
            foreach ( var user in users )
                await _cacheService.SetAsync (
                    $"users:{user.Id}",
                    user,
                    CachePolicy.Medium,
                    cancellationToken
                    );
        }
    }
}

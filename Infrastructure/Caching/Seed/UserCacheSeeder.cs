using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Identities.Cache;
using KiaKooshar.Application.Features.Interfaces.Cache;
using KiaKooshar.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Caching.Seed
{
    public class UserCacheSeeder : IUserCacheSeeder
    {
        private readonly IDatabaseContext _context;
        private readonly ICacheService _cacheService;
        public UserCacheSeeder (
            DatabaseContext context,
            ICacheService cacheService
            )
        {
            _context = context;
            _cacheService = cacheService;
        }
        public async Task SeedToCacheAsync (
            CancellationToken cancellationToken = default
            )
        {
            var users = await _context.Users
                .AsNoTracking ()
                .Where (x => !x.IsDeleted)
                .Select (u => new CachedUserDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName,
                })
                .ToListAsync (cancellationToken);

            var userRoleAndPermission = await _context
                .UserRoles
                .AsNoTracking ()
                .Where (ur => !ur.IsDeleted &&
                        !ur.Role.IsDeleted)
                .Select (ur => new
                {
                    ur.UserId,
                    RoleCode = ur.Role.Code,
                    RolePermissions = ur.Role.RolePermission
                        .Where (rp => !rp.IsDeleted)
                        .Select (rp => rp.Permission.Code)
                })
                .ToListAsync (cancellationToken);

            var userPermissions = await _context
                .UserPermissions
                .AsNoTracking ()
                .Where (ur => !ur.IsDeleted)
                .Select (ur => new
                {
                    ur.UserId,
                    Permissions = ur.Permission.Code
                }).ToListAsync (cancellationToken);

            var lookupUserRole = userRoleAndPermission
                .GroupBy (x => x.UserId)
                .ToDictionary (
                    g => g.Key,
                    g => new
                    {
                        Roles = g.Select (x => x.RoleCode)
                            .Distinct ().ToList (),
                        RolePermissions = g.SelectMany (x => x.RolePermissions)
                            .Distinct ().ToList (),
                    });

            var lookupPermission = userPermissions
                .GroupBy (x => x.UserId)
                .ToDictionary (
                    g => g.Key,
                    g => g.Select (x => x.Permissions)
                            .Distinct ()
                            .ToList ()
                    );

            foreach ( var user in users )
            {
                if ( lookupUserRole.TryGetValue
                    (user.Id, out var rolesAndPermissions) )
                {
                    user.Roles = rolesAndPermissions.Roles;
                    user.RolePermissions = rolesAndPermissions.RolePermissions;
                }
                if ( lookupPermission.TryGetValue
                    (user.Id, out var permissions) )
                    user.Permissions = permissions;

                await _cacheService.SetAsync (
                    $"users:{user.Id}",
                    user,
                    CachePolicy.Medium,
                    cancellationToken
                    );
            }
        }
    }
}

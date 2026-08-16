using AutoMapper;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Identities.Authorization;
using KiaKooshar.Application.Features.Interfaces.Authorization;

namespace KiaKooshar.Infrastructure.Persistence.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly ICacheService _cache;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public PermissionService (
            ICacheService cache,
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _cache = cache;
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<AuthorizationInfo> GetAuthorizationInfoAsync (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            string key = CacheKeys.User (userId);
            var cacheResult = await _cache.GetAsync<AuthorizationInfo> (
                key,
                cancellationToken
                );
            if ( cacheResult is not null )
                return cacheResult;
            var user = await _unit.Users.GetUserPermissions (
                userId,
                cancellationToken
                );
            //var permissions = user.UserRole
            //    .SelectMany (x => x.Role.RolePermission)
            //    .Select (x => x.Permission.Code)
            //    .Distinct ()
            //    .ToHashSet ();
            //var roles = user.UserRole
            //    .Select (x => x.Role.Code)
            //    .Distinct ()
            //    .ToHashSet ();
            //var authorizationInfo = new AuthorizationInfo
            //{
            //    Roles = roles,
            //    Permissions = permissions
            //};
            //await _cache.SetAsync (
            //    CacheKeys.User (userId),
            //    authorizationInfo,
            //    CachePolicy.Long,
            //    cancellationToken
            //    );
            //return authorizationInfo;
            AuthorizationInfo authorizationInfo = new AuthorizationInfo
            {

            };
            return authorizationInfo;
        }
        public async Task<bool> HasPermissionAsync (
            long userId,
            string permission,
            CancellationToken cancellationToken = default
            )
        {
            string key = CacheKeys.User (userId);
            var userCache = await _cache.GetAsync<bool> (key, cancellationToken);
            // var result = userCache. = ;
            // return result;
            return await Task.FromResult (true);
        }

        public async Task InvalidateAuthorizationCacheAsync (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            string key = CacheKeys.User (userId);
            await _cache.RemoveAsync (key, cancellationToken);
        }

        public Task<bool> IsInRoleAsync (
            long userId,
            string role,
            CancellationToken cancellationToken = default
            )
        {
            return Task.FromResult (true);
        }
    }
}

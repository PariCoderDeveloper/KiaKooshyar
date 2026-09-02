using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.DTOs.Identities.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

namespace KiaKooshar.Peresentation.Authorization
{
    public class PermissionAuthorizationHandler :
        AuthorizationHandler<PermissionRoleRequirement>
    {
        private readonly ICacheService _cacheService;
        public PermissionAuthorizationHandler (
            ICacheService cacheService
            )
        {
            _cacheService = cacheService;
        }
        protected override async Task HandleRequirementAsync (
            AuthorizationHandlerContext context,
            PermissionRoleRequirement requirement
            )
        {
            var id = context.User.FindFirst
                 (c => c.Type == JwtRegisteredClaimNames.Sub)?
                .Value;

            var userCache = await _cacheService.GetAsync<CachedUserDTO> (
                $"users:{id}"
                );

            if ( userCache == null )
                return;

            var hasPermission =
                !string.IsNullOrEmpty
                   (requirement.Permission) &&
                userCache.Permissions
                  .Contains (requirement.Permission) &&
                userCache.RolePermissions
                  .Contains (requirement.Permission);
            var hasRole =
                !string.IsNullOrEmpty
                    (requirement.RequiredRole) &&
                userCache.Roles
                    .Contains (requirement.RequiredRole);

            if ( hasPermission || hasRole )
                context.Succeed (requirement);
        }
    }
}

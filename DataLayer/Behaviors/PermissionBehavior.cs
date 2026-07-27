using KiaKooshar.Application.Authorization;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Caching.Policies;
using KiaKooshar.Application.DTOs.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KiaKooshar.Application.Behaviors
{
    public class PermissionBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
    {
        private readonly ICacheService _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionBehavior (
            ICacheService cache,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle (
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            if ( request is not IRequirePermission permissionRequest )
                return await next ();
            var userId = _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue (
                    ClaimTypes.NameIdentifier
                    );
            if ( string.IsNullOrEmpty (userId) )
                return CreateForbiddenResponse ("User is not authenticated");
            var cacheKey = CacheKeys.UserPermissions (long.Parse (userId));
            var permissions = await _cache.GetAsync<List<string>> (
                cacheKey,
                cancellationToken
                );
            if ( permissions == null )
                return CreateForbiddenResponse ("Permission cache not found");
            var hashPermission = permissions.Contains (
                permissionRequest.Permission
                );
            if ( !hashPermission )
                return CreateForbiddenResponse ("You don't have permission");
            return await next ();
        }
        private TResponse CreateForbiddenResponse (
            string message
            )
        {
            return (TResponse) (object)
                ResultDTO<object>.Forbid (message);
        }
    }
}

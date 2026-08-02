using KiaKooshar.Application.DTOs.Identities.Authorization;

namespace KiaKooshar.Application.Features.Interfaces.Authorization
{
    public interface IPermissionService
    {
        Task<AuthorizationInfo> GetAuthorizationInfoAsync (
            long userId,
            CancellationToken cancellationToken = default
            );
        Task<bool> HasPermissionAsync (
            long userId,
            string permission,
            CancellationToken cancellationToken = default
            );
        Task<bool> IsInRoleAsync (
            long userId,
            string role,
            CancellationToken cancellationToken = default
            );
        Task InvalidateAuthorizationCacheAsync (
            long userId,
            CancellationToken cancellationToken = default
            );
    }
}

using KiaKooshar.Application.Features.Interfaces.Repositories;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IPermissionRepository Permissions { get; }
        public IUserRoleRepository UserRoles { get; }
        public IRolePermissionRepository RolePermission { get; }
        public IRefreshTokenRepository RefreshToken { get; }
        public IUserSessionRepository UserSessions { get; }
        public IUploadedFileRepository UploadedFile { get; }
        public Task<int> CommitAsync (
            CancellationToken cancellationToken = default
            );
    }
}

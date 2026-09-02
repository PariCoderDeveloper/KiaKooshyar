using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IUserRepository Users { get; }

        public IRoleRepository Roles { get; }

        public IPermissionRepository Permissions { get; }

        public IUserRoleRepository UserRoles { get; }

        public IRolePermissionRepository RolePermission { get; }

        public IRefreshTokenRepository RefreshToken { get; }

        public IUserSessionRepository UserSessions { get; }
        public IUploadedFileRepository UploadedFile { get; }
        public IUserPermissionRepository UserPermission { get; }

        private bool _disposed = false;
        public UnitOfWork (
            DatabaseContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissions,
            IUserRoleRepository userRoles,
            IRolePermissionRepository rolePermission,
            IRefreshTokenRepository refreshToken,
            IUserSessionRepository userSessions,
            IUploadedFileRepository uploadedFile,
            IUserPermissionRepository userPermission
            )
        {
            _context = context;
            Users = userRepository;
            Roles = roleRepository;
            Permissions = permissions;
            RolePermission = rolePermission;
            UserRoles = userRoles;
            RefreshToken = refreshToken;
            UserSessions = userSessions;
            UploadedFile = uploadedFile;
            UserPermission = userPermission;
        }

        public async Task<int> CommitAsync (
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var result = await _context.SaveChangesAsync ();
            return result;
        }
        protected virtual void Dispose ( bool disposing )
        {
            if ( !_disposed )
            {
                if ( disposing )
                    _context?.Dispose ();
                _disposed = true;
            }
        }
        public void Dispose ()
        {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
    }
}

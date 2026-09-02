using KiaKooshar.Domain.Entities.Audit;
using KiaKooshar.Domain.Entities.Identies;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Domain.Entities.UploadFile;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IDatabaseContext
    {
        public DbSet<Permission> Permissions { get; }
        public DbSet<User> Users { get; }
        public DbSet<Role> Roles { get; }
        public DbSet<RolePermission> RolePermissions { get; }
        public DbSet<UserRole> UserRoles { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }
        public DbSet<UserSession> UserSessions { get; }
        public DbSet<AuditLog> AuditLogs { get; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public Task<int> SaveChangesAsync (
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken ()
            );
        public Task<int> SaveChangesAsync ( CancellationToken cancellationToken = new CancellationToken () );
    }
}

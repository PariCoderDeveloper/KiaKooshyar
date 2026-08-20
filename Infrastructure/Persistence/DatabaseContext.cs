using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Domain.Entities.UploadFile;
using KiaKooshar.Infrastructure.AuditLog;
using KiaKooshar.Infrastructure.Persistence.Mappings.GenericMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly ICurrentUserService _currentUserService;
        public DatabaseContext (
            DbContextOptions<DatabaseContext> options,
            ICurrentUserService currentUserService
            ) : base (options)
        {
            _currentUserService = currentUserService;
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<KiaKooshar.Domain.Entities.Audit.AuditLog> AuditLogs { get; set; }
        public override Task<int> SaveChangesAsync ( bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default )
            => base.SaveChangesAsync (acceptAllChangesOnSuccess, cancellationToken);
        public override async Task<int> SaveChangesAsync (
            CancellationToken cancellationToken = default
            )
        {
            var auditEntries = OnBeforeSaveChanges ();
            var result = await base.SaveChangesAsync (cancellationToken);
            await OnAfterSaveChanges (auditEntries, cancellationToken);
            return result;
        }
        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating (modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (DatabaseContext).Assembly);
            SoftDeleteFilter.ApplySoftDeleteQueryFilter (modelBuilder);
            RowVersionFilter.ApplyRowVersionConcurrencyToken (modelBuilder);
        }
        private List<AuditEntry> OnBeforeSaveChanges ()
        {
            ChangeTracker.DetectChanges ();
            var auditEntries = new List<AuditEntry> ();
            foreach ( var entry in ChangeTracker.Entries () )
            {
                if ( entry.Entity is KiaKooshar.Domain.Entities.Audit.AuditLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged
                    )
                    continue;
                var auditEntry = new AuditEntry (entry)
                {
                    TableName = entry.Metadata.GetTableName () ??
                        entry.Entity.GetType ().Name,
                    UserId = _currentUserService.UserId,
                    Username = _currentUserService.Username,
                    IP = _currentUserService.IP
                };
                foreach ( var property in entry.Properties )
                {
                    var propertyName = property.Metadata.Name;
                    if ( property.Metadata.IsPrimaryKey () )
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch ( entry.State )
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] =
                                property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] =
                                property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if ( property.IsModified &&
                                !Equals (
                                    property.OriginalValue,
                                    property.CurrentValue
                                    )
                                )
                            {
                                auditEntry.ChangedColumns.Add (propertyName);
                                auditEntry.OldValues[propertyName] =
                                    property.OriginalValue;
                                auditEntry.NewValues[propertyName] =
                                    property.CurrentValue;
                            }
                            break;
                    }
                }
                auditEntry.ChangeType = entry.State switch
                {
                    EntityState.Added => "Added",
                    EntityState.Deleted => "Deleted",
                    EntityState.Modified => "Modified",
                    _ => "Unknown"
                };
                if ( entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted
                    )
                {
                    auditEntry.HasTemporaryProperties = entry.Properties.Any (
                        p => p.IsTemporary
                        );
                    if ( auditEntry.HasTemporaryProperties )
                        auditEntries.Add (auditEntry);
                    else
                        AuditLogs.Add (auditEntry.ToAuditLog ());
                }
            }
            return auditEntries;
        }
        private Task OnAfterSaveChanges (
            List<AuditEntry> auditEntries,
            CancellationToken cancellationToken = default
            )
        {
            if ( auditEntries == null || auditEntries.Count == 0 )
                return Task.CompletedTask;

            foreach ( var auditEntry in auditEntries )
            {
                foreach ( var prop in auditEntry.TemporaryProperties )
                {
                    if ( prop.Metadata.IsPrimaryKey () )
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    else
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }
                AuditLogs.Add (auditEntry.ToAuditLog ());
            }
            return base.SaveChangesAsync (cancellationToken);
        }
    }
}

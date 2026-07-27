using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping.GenericMapping;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext ( DbContextOptions<DatabaseContext> options ) : base (options) { }

        public DbSet<Permission> Permissions { get; }

        public DbSet<User> Users { get; }

        public DbSet<Role> Roles { get; }

        public DbSet<RolePermission> RolePermissions { get; }

        public DbSet<UserRole> UserRoles { get; }

        public DbSet<RefreshToken> RefreshTokens { get; }

        public DbSet<UserSession> UserSessions { get; }

        public override Task<int> SaveChangesAsync ( bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default )
            => base.SaveChangesAsync (acceptAllChangesOnSuccess, cancellationToken);
        public override Task<int> SaveChangesAsync ( CancellationToken cancellationToken = default )
            => base.SaveChangesAsync (cancellationToken);

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
        {
            base.OnModelCreating (modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (DatabaseContext).Assembly);
            SoftDeleteFilter.ApplySoftDeleteQueryFilter (modelBuilder);

        }
        private void SeedData ( ModelBuilder modelBuilder )
        {

        }
    }
}

using KiaKooshar.Application.Construct.Context;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Permission> Permissions { get; }
        public DbSet<User> Users { get; }

        public DbSet<Role> Roles { get; }

        public DbSet<RolePermission> RolePermissions { get; }

        public DbSet<UserRole> UserRoles { get; }

        public DbSet<RefreshToken> RefreshTokens { get; }

        public DbSet<UserSession> UserSessions { get; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

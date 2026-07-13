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
            queryFilter(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // Fluent API
            ConfigureUser(modelBuilder);
            ConfigureRole(modelBuilder);
            ConfigurePermission(modelBuilder);

            ConfigureUserRole(modelBuilder);
            ConfigureRolePermission(modelBuilder);

            ConfigureUserSession(modelBuilder);
        }
        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(x => x.UserRole)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                .HasMany(x => x.RefreshToken)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                .HasMany(x => x.UserSession)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void ConfigureRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(x => x.UserRole)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Role>()
                .HasMany(x => x.RolePermission)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigurePermission(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasMany(x => x.RolePermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void ConfigureUserRole(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.RoleId);
        }

        private void ConfigureUserSession(ModelBuilder modelBuilder)
        {
        }
 
        private void queryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<RolePermission>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<RefreshToken>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<UserSession>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, 
            CancellationToken cancellationToken = new CancellationToken());
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}

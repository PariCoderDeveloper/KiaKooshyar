using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Permission> Permission { get; }
        IRepository<Role> Role { get; }
        IRepository<User> User { get; }
        IRepository<UserRole> UserRoles { get; }
        IRepository<UserSession> UserSession { get; }
        IRepository<RolePermission> RolePermissions { get; }
        IRepository<RefreshToken> RefreshToken { get; }
        public Task<int> CommitAsync ();
    }
}

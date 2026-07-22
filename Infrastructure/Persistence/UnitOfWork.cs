using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IRepository<Permission> Permission { get; private set; }

        public IRepository<Role> Role { get; private set; }

        public IRepository<User> User { get; private set; }

        public IRepository<UserRole> UserRoles { get; private set; }

        public IRepository<UserSession> UserSession { get; private set; }

        public IRepository<RolePermission> RolePermissions { get; private set; }

        public IRepository<RefreshToken> RefreshToken { get; private set; }

        private bool _disposed = false;
        public UnitOfWork ( DatabaseContext context )
        {
            _context = context;
            Permission = new GenericRepository<Permission> (_context);
            Role = new GenericRepository<Role> (_context);
            User = new GenericRepository<User> (_context);
            RolePermissions = new GenericRepository<RolePermission> (_context);
            UserRoles = new GenericRepository<UserRole> (_context);
            UserSession = new GenericRepository<UserSession> (_context);
            RefreshToken = new GenericRepository<RefreshToken> (_context);
        }

        public async Task<int> CommitAsync ()
        {
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

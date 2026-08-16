using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserRepository :
        GenericRepository<User>,
        IUserRepository
    {
        private readonly IDatabaseContext _context;
        public UserRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }
        public async Task<List<string>> GetUserPermissions (
            long id,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var userPermission = await _context.Users
                .Where (x => x.Id == id)
                .SelectMany (u => u.UserRole)
                .SelectMany (ur => ur.Role.RolePermission)
                .Select (rp => rp.Permission.DiplayName)
                .Distinct ()
                .ToListAsync ();
            return userPermission;
        }
        public async Task<User> GetUserByEmail (
            string email,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Users
                 .Where (x => x.Email == email)
                 .FirstOrDefaultAsync (cancellationToken);
        }

        public async Task<List<string>> GetUserRoles (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var roleNames = await _context.Users
                .Where (x => x.Id == userId)
                .SelectMany (x => x.UserRole)
                .Select (x => x.Role.Name)
                .ToListAsync ();
            return roleNames;
        }
    }
}

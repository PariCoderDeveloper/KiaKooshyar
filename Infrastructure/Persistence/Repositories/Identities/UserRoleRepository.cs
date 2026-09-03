using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserRoleRepository :
        GenericRepository<UserRole>,
        IUserRoleRepository
    {
        private readonly IDatabaseContext _context;
        public UserRoleRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }
        public async Task AddRangeAsync (
            List<UserRole> userRoles,
            CancellationToken cancellationToken = default
            )
        {
            await _context.UserRoles
                .AddRangeAsync (
                    userRoles,
                    cancellationToken
                );
        }
        public async Task<List<long>> GetExistingRoleIdsForUserAsync (
            long userId,
            List<long> roleId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.UserRoles
                 .Where (x =>
                     x.UserId == userId &&
                     roleId.Contains (x.RoleId))
                 .Select (x => x.Id)
                 .ToListAsync ();
        }
        public async Task<UserRole?> GetExistingRoleIdForUserAsync (
                long userId,
                long roleId,
                CancellationToken cancellationToken = default
            )
        {
            return await _context.UserRoles
                .Where (x =>
                    x.UserId == userId &&
                    x.RoleId == roleId)
                .FirstOrDefaultAsync (cancellationToken);
        }
        public async Task<UserRole?> GetUserRoleAsync (
            Expression<Func<UserRole, bool>> wherePeredicate,
            long roleId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.UserRoles
                .Where (x =>
                    x.RoleId == roleId)
                .Where (wherePeredicate)
                .FirstOrDefaultAsync
                    (cancellationToken);
        }
        public async Task<List<long>> GetUserRoles (
            long roleId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.UserRoles
                .Where (x => x.RoleId == roleId)
                .Select (x => x.Id)
                .ToListAsync (cancellationToken);
        }
    }
}

using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identies;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserPermissionRepository :
            GenericRepository<UserPermission>,
            IUserPermissionRepository
    {
        private readonly IDatabaseContext _context;
        public UserPermissionRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }

        public async Task AddRangeAsync (
            List<UserPermission> userPermissions,
            CancellationToken cancellationToken = default
            )
        {
            await _context.UserPermissions.AddRangeAsync (
                userPermissions,
                cancellationToken
                );
        }

        public async Task<List<long>> GetExistingPermissionIdsForUserAsync (
            long UserId,
            List<long> permissionId,
            CancellationToken cancellationToken = default
        )
        {
            return await _context.UserPermissions
                 .Where (x =>
                     x.UserId == UserId &&
                     permissionId.Contains (x.PermissionId)
                 ).Select (x => x.Id)
                 .ToListAsync ();
        }
    }
}

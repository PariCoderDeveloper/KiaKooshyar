using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class PermissionRepository :
        GenericRepository<Permission>,
        IPermissionRepository
    {
        private readonly IDatabaseContext _context;
        public PermissionRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }

        public async Task<List<long>> GetActivePermissionIdsAsync (
            List<long> permissionId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Permissions
                .Where (x => permissionId.Contains (x.Id))
                .Select (x => x.Id)
                .ToListAsync (cancellationToken);
        }
    }
}

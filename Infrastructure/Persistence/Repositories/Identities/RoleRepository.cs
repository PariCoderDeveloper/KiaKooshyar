using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class RoleRepository :
        GenericRepository<Role>,
        IRoleRepository
    {
        private readonly IDatabaseContext _context;
        public RoleRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }

        public async Task<List<long>> GetActiveRoleIdsAsync (
            List<long> roleId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.Roles
                .Where (
                     x => roleId.Contains (x.Id) &&
                     !x.IsDeleted
                     )
                .Select (x => x.Id)
                .ToListAsync (cancellationToken);
        }
    }
}

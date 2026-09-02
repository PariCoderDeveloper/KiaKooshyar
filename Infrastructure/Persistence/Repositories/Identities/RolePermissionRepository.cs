using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class RolePermissionRepository :
        GenericRepository<RolePermission>,
        IRolePermissionRepository
    {
        private readonly IDatabaseContext _context;
        public RolePermissionRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }

        public async Task AddRangeAsync (
            List<RolePermission> rolePermissions,
            CancellationToken cancellationToken = default
            )
        {
            await _context.RolePermissions
               .AddRangeAsync (
                rolePermissions,
                cancellationToken
                );
        }

        public async Task<List<long>> GetExistingPermissionIdsForRoleAsync (
            long roleId,
            List<long> permissionId,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.RolePermissions
                 .Where (x =>
                     x.RoleId == roleId &&
                     permissionId.Contains (x.RoleId)
                 )
                 .Select (x => x.Id)
                 .ToListAsync ();
        }

        public IQueryable<Permission> GetPermissionsForRoleAsync (
            long roleId,
            CancellationToken cancellationToken = default
            )
        {
            return _context.RolePermissions
                 .Where (x => x.RoleId == roleId)
                 .Select (x => x.Permission)
                 .AsNoTracking ();
        }
    }
}

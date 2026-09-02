using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IRolePermissionRepository :
        IRepository<RolePermission>
    {
        public Task<List<long>>
            GetExistingPermissionIdsForRoleAsync (
            long roleId,
                List<long> permissionId,
                CancellationToken cancellationToken = default
            );

        public IQueryable<Permission>
            GetPermissionsForRoleAsync (
                long roleId,
                CancellationToken cancellationToken = default
            );
        Task AddRangeAsync (
            List<RolePermission> rolePermissions,
            CancellationToken cancellationToken = default
            );
    }
}

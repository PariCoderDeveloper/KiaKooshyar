using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identies;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserPermissionRepository :
        IRepository<UserPermission>
    {
        Task AddRangeAsync (
            List<UserPermission> userPermissions,
            CancellationToken cancellationToken = default
            );
        Task<List<long>> GetExistingPermissionIdsForUserAsync (
            long UserId,
            List<long> permissionId,
            CancellationToken cancellationToken = default
        );
    }
}

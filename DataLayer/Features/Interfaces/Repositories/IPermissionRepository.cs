using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IPermissionRepository :
        IRepository<Permission>
    {
        public Task<List<long>> GetActivePermissionIdsAsync (
             List<long> permissionId,
             CancellationToken cancellationToken = default
        );
    }
}

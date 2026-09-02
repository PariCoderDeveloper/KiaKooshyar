using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<List<long>> GetActiveRoleIdsAsync (
            List<long> roleId,
            CancellationToken cancellationToken = default
            );
    }
}

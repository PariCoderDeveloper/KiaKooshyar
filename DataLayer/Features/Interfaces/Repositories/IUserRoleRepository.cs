using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task AddRangeAsync (
            List<UserRole> userRoles,
            CancellationToken cancellationToken = default
            );
        Task<List<long>> GetExistingRoleIdsForUserAsync (
            long userId,
            List<long> roleId,
            CancellationToken cancellationToken = default
        );
        Task<UserRole?> GetUserRoleAsync (
          Expression<Func<UserRole, bool>> wherePeredicate,
          long roleId,
          CancellationToken cancellationToken = default
          );
        Task<UserRole?> GetExistingRoleIdForUserAsync (
                long userId,
                long roleId,
                CancellationToken cancellationToken = default
            );
        Task<List<long>> GetUserRoles (
            long roleId,
            CancellationToken cancellationToken = default
            );
    }
}

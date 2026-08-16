using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task AddRangeAsync (
            List<UserRole> userRoles,
            CancellationToken cancellationToken = default
            );
    }
}

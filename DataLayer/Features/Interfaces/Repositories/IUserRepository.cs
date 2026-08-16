using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<string>> GetUserRoles (
            long userId,
            CancellationToken cancellationToken = default
            );
        Task<List<string>> GetUserPermissions (
            long id,
            CancellationToken cancellationToken = default
            );
        Task<User> GetUserByEmail (
            string email,
            CancellationToken cancellationToken = default
            );
    }
}

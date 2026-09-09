using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Identities.Cache;
using KiaKooshar.Application.DTOs.Identities.Users.Queries;
using KiaKooshar.Domain.Entities.Identity;
using System.Linq.Expressions;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> HasPermission (
            long userId,
            string permission,
            CancellationToken cancellationToken = default
            );
        Task<List<string>> GetUserRoles (
            long userId,
            CancellationToken cancellationToken = default
            );
        Task<List<string>> GetUserPermissions (
            long id,
            CancellationToken cancellationToken = default
            );
        Task<User?> GetUserByEmail (
            string email,
            CancellationToken cancellationToken = default
            );
        Task<User?> GetUserByChangingValues (
            Expression<Func<User, bool>> wherePredicate,
            CancellationToken cancellationToken = default
            );
        Task<CachedUserDTO?> GetCachedUserAsync (
            long userId,
            CancellationToken cancellationToken = default
            );
        IQueryable<GetAllUsersDTO?> GetAllUsersAsync (
           CancellationToken cancellationToken = default
           );
    }
}

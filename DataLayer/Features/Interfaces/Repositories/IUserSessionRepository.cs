using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IUserSessionRepository : IRepository<UserSession>
    {
        Task<UserSession> GetUserSessionByRefreshTokenId (
            long refreshTokenId,
            CancellationToken cancellationToken = default
            );
        Task<UserSession> GetUserSessionByUserId (
            long userId,
            CancellationToken cancellationToken = default
            );
    }
}

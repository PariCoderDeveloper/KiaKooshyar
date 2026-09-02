using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.Features.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        Task<RefreshToken?> FindByTokenAsync (
            string token,
            CancellationToken cancellationToken = default
            );
        Task<List<RefreshToken>> GetExpiredOrRevokedAsync (
            DateTime dateTime,
            CancellationToken cancellationToken = default
            );

        IQueryable<RefreshToken> GetRefreshTokenByUserId (
            long userId,
            CancellationToken cancellationToken = default
            );
        void RemoveRange ( IEnumerable<RefreshToken> refreshTokens );

    }
}

using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class
        RefreshTokenRepository :
        GenericRepository<RefreshToken>,
        IRefreshTokenRepository
    {
        private readonly IDatabaseContext _context;
        public RefreshTokenRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }
        public async Task<RefreshToken?> FindByTokenAsync (
            string token,
            CancellationToken cancellationToken = default
            )
        {
            var foundToken = await _context.RefreshTokens
               .Where (x => x.Token == token)
               .FirstOrDefaultAsync (cancellationToken);
            return foundToken;
        }
        public async Task<List<RefreshToken>> GetExpiredOrRevokedAsync (
            DateTime dateTime,
            CancellationToken cancellationToken = default
            )
        {
            return await _context.RefreshTokens
                .Where (rt =>
                    rt.ExpireDate <= dateTime ||
                    rt.Revoked != null
                )
                .ToListAsync (cancellationToken);
        }

        public IQueryable<RefreshToken> GetRefreshTokenByUserId (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            return _context.RefreshTokens
                .Where (x => x.UserId == userId
                    && x.IsDeleted == false
                    && x.Revoked == null)
                .AsQueryable<RefreshToken> ();
        }

        public void RemoveRange (
            IEnumerable<RefreshToken> refreshTokens
            )
        {
            _context.RefreshTokens.RemoveRange (refreshTokens);
        }
    }
}

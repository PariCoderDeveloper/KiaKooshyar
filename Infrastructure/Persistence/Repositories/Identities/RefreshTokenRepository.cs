using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class RefreshTokenRepository :
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
            CancellationToken cancellationToken
            )
        {
            var foundToken = await _context.RefreshTokens
               .Where (x => x.Token == token)
               .FirstOrDefaultAsync (cancellationToken);
            return foundToken;
        }
        public async Task<List<RefreshToken>> GetExpiredOrRevokedAsync (
            DateTime dateTime,
            CancellationToken cancellationToken
            )
        {
            return await _context.RefreshTokens
                .Where (rt =>
                    rt.ExpireDate <= dateTime ||
                    rt.Revoked != null
                )
                .ToListAsync (cancellationToken);
        }
        public void RemoveRange (
            IEnumerable<RefreshToken> refreshTokens
            )
        {
            _context.RefreshTokens.RemoveRange (refreshTokens);
        }
    }
}

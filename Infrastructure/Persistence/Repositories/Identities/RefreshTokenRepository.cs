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
        public async Task<RefreshToken> FindByToken (
            string token,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var foundToken = await _context.RefreshTokens
               .Where (x => x.Token == token)
               .FirstOrDefaultAsync ();
            return foundToken;
        }
    }
}

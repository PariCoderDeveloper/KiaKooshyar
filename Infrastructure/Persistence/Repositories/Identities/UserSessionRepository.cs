using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.Identity;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Identities
{
    public class UserSessionRepository :
        GenericRepository<UserSession>,
        IUserSessionRepository
    {
        private readonly IDatabaseContext _context;
        public UserSessionRepository (
            DatabaseContext context
            ) : base (context)
        {
            _context = context;
        }

        public async Task<UserSession> GetUserSessionByRefreshTokenId (
            long refreshTokenId,
            CancellationToken cancellationToken = default
            )
        {
            var userSession = await _context.UserSessions.
                FirstOrDefaultAsync (s => s.RefreshToken.Id == refreshTokenId);
            return userSession;
        }
        public async Task<UserSession> GetUserSessionByUserId (
            long userId,
            CancellationToken cancellationToken = default
            )
        {
            var userSession = await _context.UserSessions
                .FirstOrDefaultAsync (
                    s => s.UserId == userId,
                    cancellationToken
                    );
            return userSession;
        }

        public async Task<UserSession> GetUserSession (
            long userId,
            long sessionId,
            CancellationToken cancellationToken = default
            )
        {
            var userSession = await _context.UserSessions
                .Where (
                    x => x.UserId == userId &&
                    x.Id == sessionId
                )
                .FirstOrDefaultAsync (cancellationToken);
            return userSession;
        }
    }
}

using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Logout;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout.RevokeMySessions
{
    public class RevokeMySessionsHandler :
        IRequestHandler<RevokeMySessionsCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public RevokeMySessionsHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            RevokeMySessionsCommand request,
            CancellationToken cancellationToken
            )
        {
            var userSession = await _unit.UserSessions.GetUserSession (
                 request.userId,
                 request.sessionId,
                 cancellationToken
                 );
            if ( userSession is null )
                return ResultDTO.NotFound (
                    "User session doesnt found"
                    );
            var refreshToken = await _unit.RefreshToken.GetByIdAsync (
                userSession.RefreshToken.Id
                );
            if ( refreshToken is null )
                return ResultDTO.NotFound (
                    "Refresh token doesnt found"
                    );
            var now = DateTime.UtcNow;
            refreshToken.Revoked = now;
            userSession.LogoutTime = now;
            userSession.UpdatedAt = now;
            userSession.IsActive = false;
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success (
                "Session terminated successfully"
                );
        }
    }
}

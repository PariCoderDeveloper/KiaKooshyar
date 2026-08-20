using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout.LogoutById
{
    public class LogoutBySessionIdHandler :
        IRequestHandler<LogoutBySessionIdCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public LogoutBySessionIdHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            LogoutBySessionIdCommand request,
            CancellationToken cancellationToken
            )
        {
            var userSession = await _unit.UserSessions.GetByIdAsync (
                request.sessionId,
                cancellationToken
                );
            if ( userSession is null )
                return ResultDTO.NotFound ("Invalid User Session");
            var refreshToken = await _unit.RefreshToken.GetByIdAsync (
                userSession.RefreshTokenId,
                cancellationToken
                );
            if ( refreshToken is null )
                return ResultDTO.NotFound ("Invalid Refresh Token");
            refreshToken.Revoked = DateTime.UtcNow;
            userSession.LogoutTime = DateTime.UtcNow;
            userSession.UpdatedAt = DateTime.UtcNow;
            userSession.IsActive = false;
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success
            (
                "Logout did successfully"
            );
        }
    }
}

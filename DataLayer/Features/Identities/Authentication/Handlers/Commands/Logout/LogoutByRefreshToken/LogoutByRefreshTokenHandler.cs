using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Application.Logging;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout.LogoutByRefreshToken
{
    public class LogoutByRefreshTokenHandler :
        IRequestHandler<LogoutByRefreshTokenCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IBaseLogger _logger;
        private readonly IUserSessionRepository _userSession;
        public LogoutByRefreshTokenHandler (
            IUnitOfWork unit,
            IBaseLogger logger,
            IUserSessionRepository userSession
            )
        {
            _unit = unit;
            _logger = logger;
            _userSession = userSession;
        }
        public async Task<ResultDTO> Handle (
            LogoutByRefreshTokenCommand request,
            CancellationToken cancellationToken
            )
        {
            var logOutRefreshToken = await _unit.RefreshToken.FindByTokenAsync
                (
                    request.RefreshToken
                );
            if ( logOutRefreshToken is null )
                return ResultDTO.NotFound ("Invalid Refresh Token");
            var userSession = await _userSession.GetUserSessionByRefreshTokenId
                (logOutRefreshToken.Id);
            logOutRefreshToken.Revoked = DateTime.UtcNow;
            userSession.LogoutTime = DateTime.UtcNow;
            userSession.UpdatedAt = DateTime.UtcNow;
            userSession.IsActive = false;
            await _unit.CommitAsync ();
            _logger.LogUserLogout (
                logOutRefreshToken.Id,
                userSession.Device,
                userSession.IP
              );
            return ResultDTO.Success ("User successfully logged out");
        }
    }
}

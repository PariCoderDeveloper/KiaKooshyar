using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Features.Interfaces.HttpContext;
using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Application.Logging;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout
{
    public class LogoutHandler :
        IRequestHandler<LogoutCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IBaseLogger _logger;
        private readonly IRequestContext _requestContext;
        private readonly IUserSessionRepository _userSession;
        public LogoutHandler (
            IUnitOfWork unit,
            IBaseLogger logger,
            IUserSessionRepository userSession,
            IRequestContext requestContext
            )
        {
            _unit = unit;
            _logger = logger;
            _requestContext = requestContext;
            _userSession = userSession;
        }
        public async Task<ResultDTO> Handle (
            LogoutCommand request,
            CancellationToken cancellationToken
            )
        {
            var logOutRefreshToken = await _unit.RefreshToken.FindByTokenAsync
                (
                    request.RefreshToken
                );
            if ( logOutRefreshToken is null )
                return ResultDTO.NotFound ("Invalid Refresh Token");
            logOutRefreshToken.Revoked = DateTime.UtcNow;
            var userSession = await _userSession.GetUserSessionByRefreshTokenId
                (logOutRefreshToken.Id);
            userSession.LogoutTime = DateTime.UtcNow;
            userSession.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync ();
            AuthLogExtensions.LogUserLogout (
                _logger,
                logOutRefreshToken.Id,
                _requestContext.Device,
                _requestContext.IpAddress
              );
            return ResultDTO.Success ("User successfully logged out");
        }
    }
}

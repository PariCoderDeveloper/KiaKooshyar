using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Features.Interfaces.HttpContext;
using KiaKooshar.Application.Logging;
using KiaKooshar.Application.Specifications.Identities.Authentication;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout
{
    public class LogoutHandler :
        IRequestHandler<LogoutCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IBaseLogger _logger;
        private readonly IRequestContext _requestContext;
        public LogoutHandler (
            IUnitOfWork unit,
            IBaseLogger logger,
            IRequestContext requestContext
            )
        {
            _unit = unit;
            _logger = logger;
            _requestContext = requestContext;
        }
        public async Task<ResultDTO> Handle (
            LogoutCommand request,
            CancellationToken cancellationToken
            )
        {
            var sepecification = new LogoutSpecification (request.RefreshToken);
            var logOutUser = await _unit.RefreshToken.FirstOrDefaultAsync (
                sepecification,
                cancellationToken
                );
            if ( logOutUser is null )
                return ResultDTO.NotFound ("Invalid Refresh Token");
            logOutUser.Revoked = DateTime.UtcNow;
            await _unit.CommitAsync ();
            AuthLogExtensions.LogUserLogout (
                _logger,
                request.Id,
                _requestContext.Device,
                _requestContext.IpAddress
              );
            return ResultDTO.Success ("User successfully logged out");
        }
    }
}

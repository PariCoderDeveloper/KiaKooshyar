using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Application.Specifications.Identities.Authentication;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Authentication.Handlers.Commands.Logout
{
    public class LogoutHandler :
        IRequestHandler<LogoutCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly ICacheService _cache;
        public LogoutHandler (
            IUnitOfWork unit,
            ICacheService cache
            )
        {
            _unit = unit;
            _cache = cache;
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
            return ResultDTO.Success ("User successfully logged out");
        }
    }
}

using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.ForceLogoutUser
{
    public class ForceLogoutUserHandler :
        IRequestHandler<ForceLogoutUserCmmand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public ForceLogoutUserHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            ForceLogoutUserCmmand request,
            CancellationToken cancellationToken
            )
        {
            var refreshTokensQuery = _unit.RefreshToken
                .GetRefreshTokenByUserId (request.Id, cancellationToken);
            var now = DateTime.UtcNow;
            await refreshTokensQuery.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.Revoked, now)
                .SetProperty (x => x.UpdatedAt, now)
                , cancellationToken
            );
            var userSessionQuery = _unit.UserSessions
                .GetUserSessionsByUserId (request.Id, cancellationToken);
            await userSessionQuery.ExecuteUpdateAsync (
                setters => setters
                .SetProperty (x => x.LogoutTime, now)
                .SetProperty (x => x.IsActive, false)
                .SetProperty (x => x.UpdatedAt, now)
                , cancellationToken
            );
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success ("All Refresh Tokens Revoked");
        }
    }
}

using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.BlockUser
{
    public class BlockUserHandler :
        IRequestHandler<BlockUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public BlockUserHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            BlockUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users.GetByIdAsync (
                request.UserId,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO.NotFound ("User doesnt found");

            var userSessions = _unit.UserSessions.GetUserSessionsByUserId (
                user.Id
                );
            await userSessions.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.IsActive, true)
                .SetProperty (x => x.LogoutTime, DateTime.UtcNow)
                .SetProperty (x => x.UpdatedAt, DateTime.UtcNow)
            );

            var refreshTokens = _unit.RefreshToken.GetRefreshTokenByUserId (
                user.Id
                );
            await refreshTokens.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.Revoked, DateTime.UtcNow)
                .SetProperty (x => x.UpdatedAt, DateTime.UtcNow)
            );

            user.Status = Domain.Enums.UserStatus.Block;
            user.StatusChangedBy = request.Id;
            user.UpdatedAt = DateTime.UtcNow;

            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success ("User status changed to block");
        }
    }
}

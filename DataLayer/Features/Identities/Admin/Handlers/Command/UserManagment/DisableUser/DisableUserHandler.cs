using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.DisableUser
{
    public class DisableUserHandler :
        IRequestHandler<DisableUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly ICurrentUserService _currentUserService;
        public DisableUserHandler (
            IUnitOfWork unit,
            ICurrentUserService currentUserService
            )
        {
            _unit = unit;
            _currentUserService = currentUserService;
        }

        public async Task<ResultDTO> Handle (
            DisableUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users.GetByIdAsync (
                request.UserId,
                cancellationToken
                );
            if ( user is null )
                return ResultDTO.NotFound
                    ("User doesnt found");
            var userSessions = _unit.UserSessions.
                GetUserSessionsByUserId (
                user.Id
            );
            await userSessions.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.IsActive, false)
                .SetProperty (x => x.LogoutTime, DateTime.UtcNow)
                .SetProperty (x => x.UpdatedAt, DateTime.UtcNow)
            );

            var refreshTokens = _unit.RefreshToken
                .GetRefreshTokenByUserId (
                user.Id
                );
            await refreshTokens.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.Revoked, DateTime.UtcNow)
                .SetProperty (x => x.UpdatedAt, DateTime.UtcNow)
            );
            user.Status = Domain.Enums.UserStatus.Inactive;
            user.StatusChangedBy = _currentUserService.UserId;
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync (
                cancellationToken
                );
            return ResultDTO.Success (
                "User status changed to block"
                );
        }
    }
}

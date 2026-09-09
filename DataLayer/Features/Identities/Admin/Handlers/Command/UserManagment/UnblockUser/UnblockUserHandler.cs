using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Application.Features.Interfaces.SignalR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.UnblockUser
{
    public class UnblockUserHandler :
        IRequestHandler<UnblockUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IUserNotificationService _userNotificationService;
        public UnblockUserHandler (
            IUnitOfWork unit,
            IUserNotificationService userNotificationService
            )
        {
            _unit = unit;
            _userNotificationService = userNotificationService;
        }

        public async Task<ResultDTO> Handle (
            UnblockUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users.GetByIdAsync (request.userId);
            if ( user is null )
                return ResultDTO.NotFound ("User doesnt found");

            var userSessions = _unit.UserSessions.
                GetUserSessionsByUserId (user.Id);
            await userSessions.ExecuteUpdateAsync (
                setter => setter
                .SetProperty (x => x.IsActive, false)
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
            user.Status = Domain.Enums.UserStatus.Unblock;
            user.StatusChangedBy = request.Id;
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync (cancellationToken);

            await _userNotificationService.NotifyForceLogoutAsync (
              user.Id.ToString (),
              "Your access changed. Please enter again. "
              );

            return ResultDTO.Success ("User status changed to unblock");
        }
    }
}

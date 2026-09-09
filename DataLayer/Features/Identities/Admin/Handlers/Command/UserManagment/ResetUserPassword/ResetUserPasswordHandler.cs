using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Application.Features.Interfaces.SignalR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.ResetUserPassword
{
    public class ResetUserPasswordHandler :
        IRequestHandler<ResetUserPasswordCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserNotificationService _userNotificationService;
        public ResetUserPasswordHandler (
            IUnitOfWork unit,
            IPasswordHasher passwordHasher,
            IUserNotificationService userNotificationService
            )
        {
            _unit = unit;
            _passwordHasher = passwordHasher;
            _userNotificationService = userNotificationService;
        }
        public async Task<ResultDTO> Handle (
            ResetUserPasswordCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users
                  .GetByIdAsync (request.userId, cancellationToken);
            if ( user is null )
                return ResultDTO<string>.NotFound ("User doesnt found");
            var userSessions = _unit.UserSessions.GetUserSessionsByUserId
                (user.Id);
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

            user.ForcePasswordChange = true;
            user.LastPasswordResetChange = DateTime.UtcNow;
            user.PasswordResetedBy = request.adminUserId;
            user.PasswordHash = _passwordHasher.HashPassword (
                request.NewPassword ?? GenerateRandomPassword ()
                );
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync (cancellationToken);

            await _userNotificationService.NotifyForceLogoutAsync (
                user.Id.ToString (),
                "Your access changed. Please enter again. "
              );

            return ResultDTO<string>.Success (
                user.PasswordHash,
                "Password reseted successfully");
        }

        private string GenerateRandomPassword ()
        {
            return $"{DateTime.UtcNow.Ticks}{Guid.NewGuid ().ToString ()}";
        }
    }
}

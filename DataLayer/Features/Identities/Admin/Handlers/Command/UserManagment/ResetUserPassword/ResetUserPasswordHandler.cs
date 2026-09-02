using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.ResetUserPassword
{
    public class ResetUserPasswordHandler :
        IRequestHandler<ResetUserPasswordCommand, ResultDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _passwordHasher;
        public ResetUserPasswordHandler (
            IUnitOfWork unit,
            IPasswordHasher passwordHasher
            )
        {
            _unit = unit;
            _passwordHasher = passwordHasher;
        }
        public async Task<ResultDTO> Handle (
            ResetUserPasswordCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = await _unit.Users
                  .GetByIdAsync (request.userId, cancellationToken);
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

            user.ForcePasswordChange = true;
            user.LastPasswordResetChange = DateTime.UtcNow;
            user.PasswordResetedBy = request.adminUserId;
            user.PasswordHash = _passwordHasher.HashPassword (
                request.NewPassword ?? GenerateRandomPassword ()
                );
            user.UpdatedAt = DateTime.UtcNow;
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success ("Password reseted successfully");
        }

        private string GenerateRandomPassword ()
        {
            return $"{DateTime.UtcNow.Ticks}{Guid.NewGuid ().ToString ()}";
        }
    }
}

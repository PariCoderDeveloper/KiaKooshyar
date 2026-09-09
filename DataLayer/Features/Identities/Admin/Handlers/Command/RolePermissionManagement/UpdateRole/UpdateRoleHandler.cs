using AutoMapper;
using KiaKooshar.Application.Caching.Contracts;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Application.Features.Interfaces.SignalR;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.UpdateRole
{
    public class UpdateRoleHandler :
        IRequestHandler<UpdateRoleCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly IUserNotificationService _userNotificationService;
        public UpdateRoleHandler (
            IUnitOfWork unit,
            IMapper mapper,
            ICacheService cacheService,
            IUserNotificationService userNotificationService
            )
        {
            _unit = unit;
            _mapper = mapper;
            _cacheService = cacheService;
            _userNotificationService = userNotificationService;
        }

        public async Task<ResultDTO> Handle (
            UpdateRoleCommand request,
            CancellationToken cancellationToken
            )
        {
            var role = await _unit.Roles.GetByIdAsync (
                request.Role.id,
                cancellationToken
                );
            if ( role is null )
                return ResultDTO.NotFound ("Role doesnt found");
            role.UpdatedAt = DateTime.UtcNow;
            _mapper.Map (request.Role, role);

            var userIds = await _unit.UserRoles.GetUserRoles
                (request.Role.id, cancellationToken);
            foreach ( var userId in userIds )
            {
                await _userNotificationService.NotifyForceLogoutAsync (
                    userId.ToString (),
                    "Your access changed. Please enter again. "
                    );
            }

            await _unit.CommitAsync (cancellationToken);
            foreach ( var userId in userIds )
            {
                await _cacheService.RemoveAsync (
                    $"users:{userId}",
                    cancellationToken);
            }

            return ResultDTO.Success (
                "Role updated successfully"
                );
        }
    }
}
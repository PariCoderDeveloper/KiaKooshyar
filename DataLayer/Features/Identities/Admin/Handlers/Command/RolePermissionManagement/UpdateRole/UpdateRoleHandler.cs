using AutoMapper;
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
        private readonly IUserNotificationService _userNotificationService;
        public UpdateRoleHandler (
            IUnitOfWork unit,
            IMapper mapper,
            IUserNotificationService userNotificationService
            )
        {
            _unit = unit;
            _mapper = mapper;
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
                    "دسترسی شما تغییر کرده، لطفاً دوباره وارد شوید"
                    );
            }

            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success (
                "Role updated successfully"
                );
        }
    }
}
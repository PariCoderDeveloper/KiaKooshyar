using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.AssignPermissionsToRole
{
    public class AssignPermissionsToRoleHandler :
        IRequestHandler<AssignPermissionsToRoleCommand,
            ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public AssignPermissionsToRoleHandler (
            IUnitOfWork unit
            , IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO> Handle (
            AssignPermissionsToRoleCommand request,
            CancellationToken cancellationToken
            )
        {
            var existingIds = await _unit.RolePermission
                 .GetExistingPermissionIdsForRoleAsync (
                     request.roleId,
                     request.permissionIds,
                     cancellationToken
                 );
            if ( existingIds.Count > 0 )
                return ResultDTO.BadRequest (
                   $"Invalid Permission: {string.Join
                   (", ", existingIds)}"
                   );

            var role = await _unit.Roles.GetByIdAsync (
                request.roleId,
                cancellationToken
                );
            if ( role is null )
                return ResultDTO.NotFound ("This role doesn't exist");

            List<RolePermission> rolePerission =
                new List<RolePermission> ();
            foreach ( var permissionId in request.permissionIds )
            {
                rolePerission.Add (new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId
                });
            }
            await _unit.RolePermission.AddRangeAsync (
                rolePerission,
                cancellationToken
                );

            var result = await _unit.CommitAsync (
                cancellationToken
                );
            if ( result == 0 )
                return ResultDTO.Failure (
                    "There is an error in adding role"
                    );
            return ResultDTO.Success (
                "Permission assigned to the role successfully"
                );
        }
    }
}

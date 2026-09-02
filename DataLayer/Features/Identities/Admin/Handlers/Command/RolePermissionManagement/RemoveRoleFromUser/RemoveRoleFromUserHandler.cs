using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement
{
    public class RemoveRoleFromUserHandler :
        IRequestHandler<RemoveRoleFromUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        public RemoveRoleFromUserHandler (
            IUnitOfWork unit
            )
        {
            _unit = unit;
        }
        public async Task<ResultDTO> Handle (
            RemoveRoleFromUserCommand request,
            CancellationToken cancellationToken
            )
        {
            User? user = null;

            if ( request.UserChangedBase.userId.HasValue )
            {
                user = await _unit.Users
                    .GetUserByChangingValues (
                        x => x.Id ==
                            request.UserChangedBase.userId,
                        cancellationToken);
            }
            else if ( !string.IsNullOrWhiteSpace
                (request.UserChangedBase.email) )
            {
                user = await _unit.Users
                   .GetUserByChangingValues (
                       x => x.Email ==
                        request.UserChangedBase.email,
                       cancellationToken);
            }
            else if ( !string.IsNullOrWhiteSpace
                (request.UserChangedBase.phoneNumber) )
            {
                user = await _unit.Users
                .GetUserByChangingValues (
                    x => x.PhoneNumber ==
                        request.UserChangedBase.phoneNumber,
                    cancellationToken);
            }

            if ( user is null )
                return ResultDTO.NotFound (
                    "This user doesnt have such role"
                    );

            var role = await _unit.Roles.GetByIdAsync (
                request.roleId,
                cancellationToken
                );
            if ( role is null )
                return ResultDTO.NotFound ("Role nt exist");

            var userRole = await _unit.UserRoles.GetExistingRoleIdForUserAsync (
                user.Id,
                role.Id,
                cancellationToken
                );
            if ( userRole is null )
                return ResultDTO.NotFound (
                    "There is not role assigned for this user"
                    );

            userRole.IsDeleted = true;
            await _unit.CommitAsync (cancellationToken);
            return ResultDTO.Success (
                "Role successfully deleted"
                );
        }
    }
}

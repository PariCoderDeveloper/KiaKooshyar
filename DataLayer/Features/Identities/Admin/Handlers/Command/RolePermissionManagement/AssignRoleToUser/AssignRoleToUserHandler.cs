using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.RolePermissionManagement.AssignRoleToUser
{
    public class AssignRoleToUserHandler :
        IRequestHandler<AssignRoleToUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public AssignRoleToUserHandler (
            IUnitOfWork unit,
            IMapper mapper
            )
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<ResultDTO> Handle (
            AssignRoleToUserCommand request,
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
                return ResultDTO.NotFound ("User doesn't found");
            var isExist = await _unit.UserRoles
                .GetExistingRoleIdForUserAsync (
                 user.Id,
                 request.roleId,
                 cancellationToken
                 );
            if ( isExist is not null )
                return ResultDTO.NotFound ("The assign did before");
            var role = await _unit.Roles.GetByIdAsync (
                request.roleId,
                cancellationToken
                );
            if ( role is null )
                return ResultDTO.NotFound (
                    "There is no role with this name"
                    );
            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id,
            };
            await _unit.UserRoles.AddAsync
                (userRole, cancellationToken);
            var result = await _unit.CommitAsync
                (cancellationToken);
            if ( result < 0 )
                return ResultDTO.Failure (
                    "There is an error in adding role"
                    );
            return ResultDTO.Success (
                "Roles assigned to the user successfully"
                );
        }
    }
}

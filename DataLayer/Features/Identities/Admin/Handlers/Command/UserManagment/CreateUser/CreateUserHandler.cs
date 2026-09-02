using AutoMapper;
using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Construct.Security;
using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Application.Features.Interfaces.CurrentUser;
using KiaKooshar.Domain.Entities.Identies;
using KiaKooshar.Domain.Entities.Identity;
using MediatR;

namespace KiaKooshar.Application.Features.Identities.Admin.Handlers.Command.UserManagment.CreateUser
{
    public class CreateUserHandler :
        IRequestHandler<CreateUserCommand, ResultDTO>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserService _currentUserService;
        public CreateUserHandler (
            IUnitOfWork unit,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            ICurrentUserService currentUserService
            )
        {
            _unit = unit;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _currentUserService = currentUserService;
        }

        public async Task<ResultDTO> Handle (
            CreateUserCommand request,
            CancellationToken cancellationToken
            )
        {
            var user = _mapper.Map<User> (request.UserInformation);
            user.PasswordHash = _passwordHasher.HashPassword
                (request.UserInformation.Password);
            user.Status = Domain.Enums.UserStatus.Active;
            user.StatusChangedBy = _currentUserService.UserId;
            await _unit.Users.AddAsync (
                user,
                cancellationToken
                );

            var validRoleIds = await _unit.Roles.
                GetActiveRoleIdsAsync (
                    request.Roles,
                    cancellationToken
                );
            var invalidRoleIds = request.Roles.
                Except (validRoleIds);
            if ( invalidRoleIds.Any () )
                return ResultDTO.BadRequest (
                    $"Invalid roles: {string.Join
                    (", ", invalidRoleIds)}"
                    );

            var validPermissions = await _unit.Permissions
                .GetActivePermissionIdsAsync (
                    request.Permissions,
                    cancellationToken
                );
            var invalidPermissionIds = request.Permissions
                .Except (validPermissions);
            if ( invalidPermissionIds.Any () )
                return ResultDTO.BadRequest (
                     $"Invalid permission: {string.Join
                     (", ", invalidPermissionIds)}"
                    );

            var userRoles = validRoleIds.Select (
                roleId => new UserRole
                {
                    User = user,
                    RoleId = roleId,
                }).ToList ();

            await _unit.UserRoles.AddRangeAsync (
                userRoles,
                cancellationToken
            );

            var userPermissions = validPermissions.Select (
                permissionId => new UserPermission
                {
                    User = user,
                    PermissionId = permissionId,
                    GrantedBy = _currentUserService.UserId,
                    IsGranted = true,
                    GrantedAt = DateTime.UtcNow,

                }).ToList ();

            await _unit.UserPermission.AddRangeAsync (
                userPermissions,
                cancellationToken
                );

            var result = await _unit.CommitAsync (
                cancellationToken
                );
            if ( result <= 0 )
                return ResultDTO.BadRequest ("Error in saving information");

            return ResultDTO.Success ("User created with roles and permissions");
        }
    }
}

using Asp.Versioning;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.RolePermissionManagement;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command.UserManagment;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Queries;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Application.Features.Identities.Users.Requests.Queries;
using KiaKooshar.Peresentation.Attributes;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KiaKooshar.Peresentation.Controllers.Admin.AuthorizationManangement.V1
{
    [ApiController]
    [ApiVersion (1.0)]
    [Route ("api/v{version:apiVersion}/[controller]")]
    public class AdminAuthorizationController
        : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminAuthorizationController (
            IMediator mediator
            )
        {
            _mediator = mediator;
        }
        [HasPermission ("User.View", "Admin")]
        [Authorize]
        [HttpGet ("GetAllPermissions")]
        public async Task<IActionResult> GetAllPermissions (
            GetAllPermissionsQuery getAllPermissionsQuery
            )
        {

            var getAllPermissionsResult = await _mediator.
                 Send (getAllPermissionsQuery);
            return ResultExtensions.ToActionResult (getAllPermissionsResult);
        }
        [HasPermission ("User.View", "Admin")]
        [Authorize]
        [HttpGet ("GetRolePermissions")]
        public async Task<IActionResult> GetRolePermissions (
             GetRolePermissionsQuery getRolePermissionsQuery
        )
        {
            var getRolePermissionsResult = await _mediator.
                 Send (getRolePermissionsQuery);
            return ResultExtensions.ToActionResult (getRolePermissionsResult);
        }
        [HasPermission ("User.View", "Admin")]
        [Authorize]
        [HttpGet ("GetUserById")]
        public async Task<IActionResult> GetUserById (
            GetUserByIdQuery getUserByIdQuery
        )
        {
            var getUserByIdResult = await _mediator.
                 Send (getUserByIdQuery);
            return ResultExtensions.ToActionResult (getUserByIdResult);
        }
        [HasPermission ("User.View", "Admin")]
        [Authorize]
        [HttpGet ("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers (
            GetAllUserCommand getAllUserCommand
        )
        {
            var getAllUsersResult = await _mediator.
                 Send (getAllUserCommand);
            return ResultExtensions.ToActionResult (getAllUsersResult);
        }
        [HasPermission ("User.Delete", "Admin")]
        [Authorize]
        [HttpDelete ("DeleteRole")]
        public async Task<IActionResult> DeleteRole (
            DeleteRoleCommand deleteRoleCommand
        )
        {
            var deleteRoleResult = await _mediator.
                 Send (deleteRoleCommand);
            return ResultExtensions.ToActionResult (deleteRoleResult);
        }
        [HasPermission ("User.Update", "Admin")]
        [Authorize]
        [HttpPut ("UpdateUser")]
        public async Task<IActionResult> UpdateUser (
            UpdateUserCommand updateUserCommand
        )
        {
            var updateUserResult = await _mediator.
                 Send (updateUserCommand);
            return ResultExtensions.ToActionResult (updateUserResult);
        }
        [HasPermission ("User.Create", "Admin")]
        [Authorize]
        [HttpPost ("CreateUser")]
        public async Task<IActionResult> CreateUser (
            CreateUserCommand createUserCommand
        )
        {
            var createUserResult = await _mediator.
                 Send (createUserCommand);
            return ResultExtensions.ToActionResult (createUserResult);
        }
        [HasPermission ("User.Delete", "Admin")]
        [Authorize]
        [HttpDelete ("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser (
            RemoveRoleFromUserCommand removeRoleFromUser
            )
        {
            var userId = User.FindFirstValue ("sub");
            var removeRoleFromUserResult = await _mediator.
                 Send (removeRoleFromUser);
            return ResultExtensions.ToActionResult (removeRoleFromUserResult);
        }
        [HasPermission ("User.Create", "Admin")]
        [Authorize]
        [HttpPost ("AssignRoleToUserHandler")]
        public async Task<IActionResult> AssignRoleToUser (
            AssignRoleToUserCommand assignRoleToUserCommand
            )
        {
            var assignRoleToUserResult = await _mediator.
                 Send (assignRoleToUserCommand);
            return ResultExtensions.ToActionResult (assignRoleToUserResult);
        }
        [HasPermission ("User.Create", "Admin")]
        [Authorize]
        [HttpPost ("AssignPermissionsToRole")]
        public async Task<IActionResult> AssignPermissionsToRole (
            AssignPermissionsToRoleCommand assignPermissionsToRole
        )
        {
            var assignPermissionsToRoleResult = await _mediator.
                 Send (assignPermissionsToRole);
            return ResultExtensions.ToActionResult (
                assignPermissionsToRoleResult
                );
        }
    }
}

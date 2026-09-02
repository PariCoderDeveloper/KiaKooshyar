using Asp.Versioning;
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

namespace KiaKooshar.Peresentation.Controllers.Admin.UserManagment.V1
{
    [ApiController]
    [ApiVersion (1.0)]
    [Route ("api/v{version:apiVersion}/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController (
            IMediator mediator
            )
        {
            _mediator = mediator;
        }
        [HasPermission ("User.Create", "SuperAdmin")]
        [Authorize]
        [HttpPost ("force-logout")]
        public async Task<IActionResult> ForceLogoutUser (
            ForceLogoutUserCmmand logoutUserCmmand
            )
        {
            var forceLogoutResult = await _mediator.Send
                 (logoutUserCmmand);
            return ResultExtensions.ToActionResult (
                forceLogoutResult
                );
        }
        [HasPermission ("User.Create", "SuperAdmin")]
        [Authorize]
        [HttpPost ("reset-user-password")]
        public async Task<IActionResult> ResetUserPassword (
            long userid,
            string password
        )
        {
            var id = User.FindFirstValue ("sub");
            long.TryParse (id, out long adminId);
            var resetUserPasswordCommand = new ResetUserPasswordCommand
            {
                userId = userid,
                NewPassword = password,
                adminUserId = adminId
            };
            var forceLogoutResult = await _mediator.Send
                 (resetUserPasswordCommand);
            return ResultExtensions.ToActionResult (
                forceLogoutResult
                );
        }
        [HasPermission ("User.Block", "SuperAdmin")]
        [Authorize]
        [HttpPut ("unblock-user")]
        public async Task<IActionResult> UnblockUser (
            UnblockUserCommand unblockUserCommand
        )
        {
            var unblockUserResult = await _mediator.Send
                 (unblockUserCommand);
            return ResultExtensions.ToActionResult (
                unblockUserResult
                );
        }
        [HasPermission ("User.Disable", "SuperAdmin")]
        [Authorize]
        [HttpPut ("enable-user")]
        public async Task<IActionResult> EnableUser (
           EnableUserCommand enableUserCommand
        )
        {
            var enableUserResult = await _mediator.Send
                 (enableUserCommand);
            return ResultExtensions.ToActionResult (
                enableUserResult
                );
        }
        [HasPermission ("User.Disable", "SuperAdmin")]
        [Authorize]
        [HttpPut ("disable-user")]
        public async Task<IActionResult> DisableUser (
            DisableUserCommand disableUserCommand
        )
        {
            var disableUserResult = await _mediator.Send
                 (disableUserCommand);
            return ResultExtensions.ToActionResult (
                disableUserResult
                );
        }
        [HasPermission ("User.Delete", "SuperAdmin")]
        [Authorize]
        [HttpDelete ("delete-user")]
        public async Task<IActionResult> DeleteUser (
            DeleteUserCommand deleteUserCommand
        )
        {
            var deleteUserResult = await _mediator.Send
                 (deleteUserCommand);
            return ResultExtensions.ToActionResult (
                deleteUserResult
                );
        }
        [HasPermission ("User.Delete", "SuperAdmin")]
        [Authorize]
        [HttpPut ("update-user")]
        public async Task<IActionResult> UpdateUser (
          UpdateUserCommand updateUserCommand
        )
        {
            var updateUserResult = await _mediator.Send
                 (updateUserCommand);
            return ResultExtensions.ToActionResult (
                updateUserResult
                );
        }
        [HasPermission ("User.View", "SuperAdmin")]
        [Authorize]
        [HttpPost ("get-user-by-id")]
        public async Task<IActionResult> GetUserById (
            GetUserByIdQuery getUserByIdQuery
        )
        {
            var getUserByIdResult = await _mediator.Send
                (getUserByIdQuery);
            return ResultExtensions.ToActionResult (
                getUserByIdResult
                );
        }
        [HasPermission ("User.View", "SuperAdmin")]
        [Authorize]
        [HttpGet ("get-all-users")]
        public async Task<IActionResult> GetAllUsers (
           GetAllUserCommand getAllUserCommand
        )
        {
            var getAllUsersResult = await _mediator.Send
                (getAllUserCommand);
            return ResultExtensions.ToActionResult (
                getAllUsersResult
                );
        }
        [HasPermission ("User.Create", "SuperAdmin")]
        [Authorize]
        [HttpPost ("create-user")]
        public async Task<IActionResult> CreateUser (
            CreateUserCommand createUserCommand
            )
        {
            var createUserResult = await _mediator.Send (
                createUserCommand
                );
            return ResultExtensions.ToActionResult (
                createUserResult
                );
        }
    }
}

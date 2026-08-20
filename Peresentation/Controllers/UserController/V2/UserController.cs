using Asp.Versioning;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Register;
using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers.UserController.V2
{
    [ApiVersion (2.0)]
    [Route ("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController (
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpPost ("/register")]
        public async Task<IActionResult> RegisterUser (
            RegisterUserCommand registerUserCommand
            )
        {
            var registrationResult =
                await _mediator.Send (registerUserCommand);
            return ResultExtensions.ToActionResult (registrationResult);
        }

        [HttpPut ("/update")]
        public async Task<IActionResult> UpdateUser (
            UpdateUserCommand updateUserCommand
            )
        {
            var updateResult =
                await _mediator.Send (updateUserCommand);
            return ResultExtensions.ToActionResult (updateResult);
        }

        [HttpPatch ("/update/changestatus")]
        public async Task<IActionResult> ChangeStatus (
            ChangeStatusUserCommand changeStatusUserCommand
            )
        {
            var changeStatusResult =
                await _mediator.Send (changeStatusUserCommand);
            return ResultExtensions.ToActionResult (changeStatusResult);
        }

        [HttpPost ("/delete")]
        public async Task<IActionResult> DeleteUser (
            DeleteUserCommand deleteUserCommand
            )
        {
            var deleteResult =
                await _mediator.Send (deleteUserCommand);
            return ResultExtensions.ToActionResult (deleteResult);
        }

        [HttpPatch ("update/changephone")]
        public async Task<IActionResult> ChangePhoneNumber (
            ChangePhoneNumberCommand changePhoneNumberCommand
            )
        {
            var changePhoneNumberResult =
                await _mediator.Send (changePhoneNumberCommand);
            return ResultExtensions.ToActionResult (changePhoneNumberResult);
        }
        [HttpPatch ("update/changepassword")]
        public async Task<IActionResult> ChangePassword (
            ChangePasswordCommand changePasswordCommand
            )
        {
            var changePasswordResult =
                await _mediator.Send (changePasswordCommand);
            return ResultExtensions.ToActionResult (changePasswordResult);
        }

        [HttpPatch ("update/changeemail")]
        public async Task<IActionResult> ChangeEmail (
            ChangeEmailCommand changeEmailCommand
            )
        {
            var changeEmailResult =
                await _mediator.Send (changeEmailCommand);
            return ResultExtensions.ToActionResult (changeEmailResult);
        }
    }
}

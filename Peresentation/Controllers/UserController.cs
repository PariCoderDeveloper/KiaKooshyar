using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers
{
    [Route ("api/[controller]")]
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
        public async Task<IActionResult> LoginUser (
            RegisterUserCommand registerUserCommand
            )
        {
            var resuult = await _mediator.Send (registerUserCommand);
            return ResultExtensions.ToActionResult (resuult);
        }

        [HttpPut ("/update")]
        public async Task<IActionResult> UpdateUser (
            UpdateUserCommand updateUserCommand
            )
        {
            var result = await _mediator.Send (updateUserCommand);
            return ResultExtensions.ToActionResult (result);
        }

        [HttpPatch ("/update/changestatus")]
        public async Task<IActionResult> ChangeStatus (
            ChangeStatusUserCommand changeStatusUserCommand
            )
        {
            var result = await _mediator.Send (changeStatusUserCommand);
            return ResultExtensions.ToActionResult (result);
        }

        [HttpPost ("/delete")]
        public async Task<IActionResult> DeleteUser (
            DeleteUserCommand deleteUserCommand
            )
        {
            var result = await _mediator.Send (deleteUserCommand);
            return ResultExtensions.ToActionResult (result);
        }

        [HttpPatch ("update/changephone")]
        public async Task<IActionResult> ChangePhoneNumber (
            ChangePhoneNumberCommand changePhoneNumberCommand
            )
        {
            var result = await _mediator.Send (changePhoneNumberCommand);
            return ResultExtensions.ToActionResult (result);
        }

        [HttpPatch ("update/changepassword")]
        public async Task<IActionResult> ChangePassword (
            ChangePasswordCommand changePasswordCommand
            )
        {
            var result = await _mediator.Send (changePasswordCommand);
            return ResultExtensions.ToActionResult (result);
        }

        [HttpPatch ("update/changeemail")]
        public async Task<IActionResult> ChangeEmail (
            ChangeEmailCommand changeEmailCommand
            )
        {
            var result = await _mediator.Send (changeEmailCommand);
            return ResultExtensions.ToActionResult (result);
        }
    }
}

using KiaKooshar.Application.Features.Identities.Users.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController ( IMediator mediator )
        {
            _mediator = mediator;
        }
        [HttpPost ("/user")]
        public async Task<IActionResult> LoginUser ( RegisterUserCommand loginViewModel )
        {
            var resuult = await _mediator.Send (loginViewModel);
            if ( resuult.IsSuccess )
            {
                return Ok (true);
            }
            return BadRequest (false);
        }

        [HttpPut ("update/user")]
        public async Task<IActionResult> UpdateUser ( UpdateUserCommand updateUserCommand )
        {
            var result = await _mediator.Send (updateUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
        [HttpPatch ("update/changestatus")]
        public async Task<IActionResult> ChangeStatus ( ChangeStatusUserCommand changeStatusUserCommand )
        {
            var result = await _mediator.Send (changeStatusUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
        [HttpPost ("delete/user")]
        public async Task<IActionResult> DeleteUser ( DeleteUserCommand deleteUserCommand )
        {
            var result = await _mediator.Send (deleteUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }

        [HttpPatch ("update/changephone")]
        public async Task<IActionResult> ChangePhoneNumber ( ChangePhoneNumberCommand changePhoneNumberCommand )
        {
            var result = await _mediator.Send (changePhoneNumberCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
        [HttpPatch ("update/changepassword")]
        public async Task<IActionResult> ChangePassword ( ChangePasswordCommand changePasswordCommand )
        {
            var result = await _mediator.Send (changePasswordCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
        [HttpPatch ("update/changeemail")]
        public async Task<IActionResult> ChangeEmail ( ChangeEmailCommand changeEmailCommand )
        {
            var result = await _mediator.Send (changeEmailCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
    }
}

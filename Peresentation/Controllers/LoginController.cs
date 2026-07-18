using KiaKooshar.Application.Requests.Identities.User.Commands;
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
        [HttpPost]

        public async Task<IActionResult> LoginUser ( RegisterUserCommand loginViewModel )
        {
            var resuult = await _mediator.Send (loginViewModel);
            if ( resuult.IsSuccess )
            {
                return Ok (true);
            }
            return BadRequest (false);
        }

        public async Task<IActionResult> UpdateUser ( UpdateUserCommand updateUserCommand )
        {
            var result = await _mediator.Send (updateUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }

        public async Task<IActionResult> ChangeStatus ( ChangeStatusUserCommand changeStatusUserCommand )
        {
            var result = await _mediator.Send (changeStatusUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
        public async Task<IActionResult> DeleteUser ( DeleteUserCommand deleteUserCommand )
        {
            var result = await _mediator.Send (deleteUserCommand);
            if ( result.IsSuccess )
            {
                return Ok (result);
            }
            return BadRequest (result);
        }
    }
}

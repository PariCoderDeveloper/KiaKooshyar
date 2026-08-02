using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController (
            IMediator mediator
            )
        {
            _mediator = mediator;
        }
        [HttpPost ("/login")]
        public async Task<IActionResult> Login (
            LoginCommand loginCommand
            )
        {
            var loginResult =
                await _mediator.Send (loginCommand);
            return ResultExtensions.ToActionResult (loginResult);
        }
        [HttpPost ("/logout")]
        public async Task<IActionResult> Logout (
            LogoutCommand logoutCommand
            )
        {
            var logoutResult =
                await _mediator.Send (logoutCommand);
            return ResultExtensions.ToActionResult (logoutResult);
        }
    }
}

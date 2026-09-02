using Asp.Versioning;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Login;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.Logout;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.RefreshToken;
using KiaKooshar.Application.Features.Identities.Authentication.Requests.Commands.Authentication.RevokeToken;
using KiaKooshar.Infrastructure.RateLimiting;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KiaKooshar.Peresentation.Controllers.AuthController.V2
{
    [ApiController]
    [ApiVersion (2.0)]
    [Route ("api/v{version:apiVersion}/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController (
            IMediator mediator
            )
        {
            _mediator = mediator;
        }
        [HttpPost ("login")]
        public async Task<IActionResult> Login (
            LoginCommand loginCommand
            )
        {
            var loginResult =
                await _mediator.Send (loginCommand);
            if ( loginResult.IsSuccess )
            {
                Response.Cookies.Append (
                    "access-token",
                    loginResult.Data.AccessToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = loginResult.Data.AccessTokenExpiration
                    }
                    );
                Response.Cookies.Append (
                    "refresh-token",
                    loginResult.Data.RefreshToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = loginResult.Data.RefreshTokenExpiration
                    }
                    );
            }
            return ResultExtensions.ToActionResult (loginResult);
        }
        [EnableRateLimiting (RateLimitPolicy.RefreshToken)]
        [HttpPost ("refresh-token")]
        public async Task<IActionResult> RefreshToken ()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            if ( string.IsNullOrEmpty (refreshToken) )
                return Unauthorized ();
            var result = await _mediator.Send (new RefreshTokenCommand
            {
                RefreshToken = refreshToken,
            });
            if ( result.IsSuccess )
                Response.Cookies.Append (
                    "access-token",
                    result.Data.AccessToken,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = result.Data.AccessTokenExpiration
                    });
            return ResultExtensions.ToActionResult (result);
        }
        [HttpPost ("logout")]
        public async Task<IActionResult> Logout ()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            var logoutResult =
                await _mediator.Send (new LogoutCurrentSessionCommand
                {
                    RefreshToken = refreshToken ?? null
                });
            Response.Cookies.Delete ("access-token");
            Response.Cookies.Delete ("refresh-token");
            return ResultExtensions.ToActionResult (logoutResult);
        }
        [HttpPost ("revoke-token")]
        public async Task<IActionResult> RevokeToken ()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            var revokeResult =
                await _mediator.Send (new RevokeTokenCommand
                {
                    RefreshToken = refreshToken
                });
            Response.Cookies.Delete ("refresh-token");
            return ResultExtensions.ToActionResult (revokeResult);
        }
    }
}

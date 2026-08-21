using Asp.Versioning;
using KiaKooshar.Application.Features.Identities.Admin.Requests.Command;
using KiaKooshar.Peresentation.Extentions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers.Admin.V1
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
        [HttpPost("force-logout")]
        public async Task<IActionResult> ForceLogoutUser (
            ForceLogoutUserCmmand logoutUserCmmand
            )
        {
            var forceLogoutResult = await _mediator.Send
                 (logoutUserCmmand);
            return ResultExtensions.ToActionResult (forceLogoutResult);
        }
    }
}

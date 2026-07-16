using KiaKooshar.Application.DTOs.Common;
using KiaKooshar.Application.Requests.Identities.Commands;
using KiaKooshar.Peresentation.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<IActionResult<bool>> LoginUser(InsertUserCommand loginViewModel)
        {
            var resuult = await _mediator.Send(loginViewModel);
            if (resuult.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

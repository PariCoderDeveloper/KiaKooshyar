using Asp.Versioning;
using KiaKooshar.Application.Features.Interfaces.Captcha;
using KiaKooshar.Peresentation.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers.CaptchaController.V1
{
    [ApiController]
    [ApiVersion (1.0)]
    [Route ("api/V{version:apiVersion}/[controller]")]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;

        public CaptchaController ( ICaptchaService captchaService )
        {
            _captchaService = captchaService;
        }
        [HttpGet ("generate")]
        public async Task<IActionResult> Generate ()
        {
            var result = await _captchaService.GenerateAsync ();
            return Ok (result);
        }
        [HttpGet ("validate")]
        public async Task<IActionResult> Validate (
            CaptchaValidate captchaValidate
            )
        {
            var result = await _captchaService.ValidateAsync (
                captchaValidate.captchaId,
                captchaValidate.userInput
                );
            return Ok (result);
        }
    }
}


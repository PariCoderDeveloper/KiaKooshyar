using KiaKooshar.Application.DTOs.Identities.Captcha;

namespace KiaKooshar.Application.Features.Interfaces.Captcha
{
    public interface ICaptchaService
    {
        Task<CaptchaResultDto> GenerateAsync ();
        Task<bool> ValidateAsync ( string captchaId, string userInput );
    }
}

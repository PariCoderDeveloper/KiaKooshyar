namespace KiaKooshar.Application.DTOs.Identities.Captcha
{
    public class CaptchaResultDto
    {
        public string CaptchaId { get; set; } = default!;
        public string ImageBase64 { get; set; } = default!;
    }
}

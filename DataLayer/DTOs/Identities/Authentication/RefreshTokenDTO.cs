namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class ResponseRefreshTokenDTO
    {
        public string AccessToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }
    }
}

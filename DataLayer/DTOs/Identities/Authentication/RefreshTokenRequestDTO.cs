namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class RefreshTokenRequestDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public long UserId { get; set; }
        public string Device { get; set; } = "Unknown";
        public string Ip { get; set; } = "Unknown";
    }
}

namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class RefreshTokenRequestDTO
    {
        public long UserId { get; set; }
        public string Device { get; set; } = "Unknown";
        public string Ip { get; set; } = "Unknown";
    }
}

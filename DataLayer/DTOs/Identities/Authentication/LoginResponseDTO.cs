using KiaKooshar.Application.DTOs.Identities.Users.Queries;

namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public UserInfoDTO User { get; set; } = null!;
    }
}

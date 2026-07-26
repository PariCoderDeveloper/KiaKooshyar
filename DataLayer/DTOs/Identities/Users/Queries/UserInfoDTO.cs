using KiaKooshar.Application.DTOs.Identities.Authentication;

namespace KiaKooshar.Application.DTOs.Identities.Users.Queries
{
    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public List<RoleDTO> Roles { get; set; } = null!;
    }
}

using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Application.DTOs.Identities.Authentication
{
    public class AuthenticatedUserDTO
    {
        public User User { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}

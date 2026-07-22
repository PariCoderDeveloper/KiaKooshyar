using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Commons
{
    public class ReturnUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; }
        public UserStatus Status { get; set; }
    }
}

using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Identities.Users.Queries
{
    public class GetUserByIdDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Avator { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string NationalCode { get; set; } = null!;
        public UserStatus Status { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}

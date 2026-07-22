using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Identities.Users.Commands
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        //[IgnoreLogging]
        public string PasswordHash { get; set; } = null!;
        public string? Avator { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public string NationalCode { get; set; } = null!;
        public UserStatus Status { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
    }
}

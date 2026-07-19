using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Identities.Users
{
    public class ResponseUpdateUserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; } = null!;
        public UserStatus Status { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }
    }
}

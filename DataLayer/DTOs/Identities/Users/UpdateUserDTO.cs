using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Application.DTOs.Identities.User
{
    public class UpdateUserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string BirthDate { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public UserStatus Status { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneConfirmed { get; set; }

    }
}

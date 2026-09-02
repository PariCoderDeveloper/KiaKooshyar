using KiaKooshar.Domain.Entities.BaseEntities;
using KiaKooshar.Domain.Entities.Identies;
using KiaKooshar.Domain.Enums;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
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
        public bool? ForcePasswordChange { get; set; }
        public DateTime? LastPasswordResetChange { get; set; }
        public long? PasswordResetedBy { get; set; }
        public long? StatusChangedBy { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; } =
            new List<UserPermission> ();
        public virtual ICollection<UserRole> UserRole { get; set; } =
            new List<UserRole> ();
        public virtual ICollection<RefreshToken> RefreshToken { get; set; } =
            new List<RefreshToken> ();
        public virtual ICollection<UserSession> UserSession { get; set; } =
            new List<UserSession> ();
    }
}

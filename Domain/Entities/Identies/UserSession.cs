using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class UserSession : BaseEntity
    {
        public string Device { get; set; } = null!;
        public string IP { get; set; } = null!;
        public string Browser { get; set; } = null!;
        public string OS { get; set; } = null!;
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual RefreshToken RefreshToken { get; set; } = null!;
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}

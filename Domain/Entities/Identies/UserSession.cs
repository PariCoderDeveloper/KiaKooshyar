using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class UserSession : BaseEntity
    {
        public string Device { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string OS { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public string LastActivity { get; set; }
        public long RefreshTokenId { get; set; }
        public virtual RefreshToken RefreshToken { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}

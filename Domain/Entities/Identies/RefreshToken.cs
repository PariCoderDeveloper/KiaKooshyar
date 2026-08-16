using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public DateTime ExpireDate { get; set; }
        public DateTime? Revoked { get; set; }
        public string Device { get; set; } = null!;
        public string IP { get; set; } = null!;
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public bool IsRevoked => Revoked != null;
        public bool IsExpired => DateTime.UtcNow >= ExpireDate;
    }
}

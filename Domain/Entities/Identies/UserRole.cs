using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class UserRole : BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public long RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}

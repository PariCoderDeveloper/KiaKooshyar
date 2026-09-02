using KiaKooshar.Domain.Entities.BaseEntities;
using KiaKooshar.Domain.Entities.Identity;

namespace KiaKooshar.Domain.Entities.Identies
{
    public class UserPermission : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;

        public long PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        public bool IsGranted { get; set; }
        public DateTime GrantedAt { get; set; }
        public long? GrantedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}

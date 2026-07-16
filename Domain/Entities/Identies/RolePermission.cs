using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class RolePermission : BaseEntity
    {
        public long RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
        public long PermissionId { get; set; }
        public virtual Permission Permission { get; set; } = null!;
    }
}

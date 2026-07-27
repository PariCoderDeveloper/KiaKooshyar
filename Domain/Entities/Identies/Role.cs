using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public virtual ICollection<UserRole> UserRole { get; set; } = null!;
        public virtual ICollection<RolePermission> RolePermission { get; set; } = null!;
    }
}

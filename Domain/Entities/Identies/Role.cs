using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}

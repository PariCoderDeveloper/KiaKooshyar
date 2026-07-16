using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = null!;
    }
}

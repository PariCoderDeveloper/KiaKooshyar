using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.Identity
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}

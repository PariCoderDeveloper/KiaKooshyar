using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class RolePermissionMap : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure ( EntityTypeBuilder<RolePermission> builder )
        {

            builder
                .HasOne (x => x.Role)
                .WithMany (x => x.RolePermission)
                .HasForeignKey (x => x.RoleId);

            builder
                .HasOne (x => x.Permission)
                .WithMany (x => x.RolePermissions)
                .HasForeignKey (x => x.PermissionId);
            builder.HasData (
                new RolePermission
                {
                    Id = 1,
                    RoleId = 1,
                    PermissionId = -1
                },
                new RolePermission
                {
                    Id = 2,
                    RoleId = 1,
                    PermissionId = -2
                }
            );
        }
    }
}

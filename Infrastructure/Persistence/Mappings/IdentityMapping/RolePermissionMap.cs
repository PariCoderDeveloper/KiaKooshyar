using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class RolePermissionMap : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.RolePermission)
                .HasForeignKey(x => x.RoleId);

            builder
                .HasOne(x => x.Permission)
                .WithMany(x => x.RolePermissions)
                .HasForeignKey(x => x.PermissionId);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}

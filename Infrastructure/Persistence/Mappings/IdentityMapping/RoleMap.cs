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
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                 .HasMany(x => x.UserRole)
                 .WithOne(x => x.Role)
                 .HasForeignKey(x => x.RoleId)
                 .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasMany(x => x.RolePermission)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}

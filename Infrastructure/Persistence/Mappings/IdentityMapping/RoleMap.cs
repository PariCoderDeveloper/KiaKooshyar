using KiaKooshar.Domain.Constants;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure ( EntityTypeBuilder<Role> builder )
        {

            builder
                 .HasMany (x => x.UserRole)
                 .WithOne (x => x.Role)
                 .HasForeignKey (x => x.RoleId)
                 .OnDelete (DeleteBehavior.Cascade);
            builder
                .HasMany (x => x.RolePermission)
                .WithOne (x => x.Role)
                .HasForeignKey (x => x.RoleId)
                .OnDelete (DeleteBehavior.Cascade);
            builder.HasIndex (x => x.Code)
                .IsUnique ();
            SeedData (builder);
        }
        private void SeedData ( EntityTypeBuilder<Role> builder )
        {
            builder.HasData (
                new Role
                {
                    Id = 1,
                    Name = "Super Administrator",
                    Code = Roles.SuperAdmin
                },
                new Role
                {
                    Id = 2,
                    Name = "Administrator",
                    Code = Roles.Admin
                },
                new Role
                {
                    Id = 3,
                    Name = "Manager",
                    Code = Roles.Manager
                },
                new Role
                {
                    Id = 4,
                    Name = "User",
                    Code = Roles.User
                }
            );
        }
    }
}

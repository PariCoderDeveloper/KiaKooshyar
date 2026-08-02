using KiaKooshar.Domain.Constants;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure ( EntityTypeBuilder<Permission> builder )
        {
            builder
                .HasMany (x => x.RolePermissions)
                .WithOne (x => x.Permission)
                .HasForeignKey (x => x.PermissionId)
                .OnDelete (DeleteBehavior.Cascade);
            builder.HasIndex (x => x.Code)
                .IsUnique ();
            SeedData (builder);
        }
        private void SeedData ( EntityTypeBuilder<Permission> builder )
        {
            builder.HasData (new Permission
            {
                Id = -1,
                DiplayName = "View Users",
                Code = Permissions.UserView,
            }, new Permission
            {
                Id = -2,
                DiplayName = "Create User",
                Code = Permissions.UserCreate,
            }, new Permission
            {
                Id = -3,
                DiplayName = "Update User",
                Code = Permissions.UserUpdate,
            }, new Permission
            {
                Id = -4,
                DiplayName = "Delete User",
                Code = Permissions.UserDelete,
            }, new Permission
            {
                Id = -5,
                DiplayName = "Disable User",
                Code = Permissions.UserDisable,
            }, new Permission
            {
                Id = -6,
                DiplayName = "User Block",
                Code = Permissions.UserBlock,
            });
        }
    }
}

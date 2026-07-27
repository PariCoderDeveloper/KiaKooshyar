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
                DiplayName = "View Users",
                Code = Permissions.UserView,
            });
            builder.HasData (new Permission
            {
                DiplayName = "Create User",
                Code = Permissions.UserCreate,
            });
            builder.HasData (new Permission
            {
                DiplayName = "Update User",
                Code = Permissions.UserUpdate,
            });
            builder.HasData (new Permission
            {
                DiplayName = "Delete User",
                Code = Permissions.UserDelete,
            });
            builder.HasData (new Permission
            {
                DiplayName = "Disable User",
                Code = Permissions.UserDisable,
            });
            builder.HasData (new Permission
            {
                DiplayName = "User Block",
                Code = Permissions.UserBlock,
            });
        }
    }
}

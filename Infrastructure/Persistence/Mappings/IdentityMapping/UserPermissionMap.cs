using KiaKooshar.Domain.Entities.Identies;
using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class UserPermissionMap :
        IEntityTypeConfiguration<UserPermission>
    {
        public void Configure (
            EntityTypeBuilder<UserPermission> builder
            )
        {

            builder.HasIndex (up => new { up.UserId, up.PermissionId })
                .IsUnique ();

            builder.HasOne (up => up.User)
                .WithMany (u => u.UserPermissions)
                .HasForeignKey (up => up.UserId)
                .OnDelete (DeleteBehavior.Cascade);

            builder.HasOne (up => up.Permission)
                .WithMany (p => p.UserPermissions)
                .HasForeignKey (up => up.PermissionId)
                .OnDelete (DeleteBehavior.Restrict);

            builder.HasOne<User> ()
                .WithMany ()
                .HasForeignKey (up => up.GrantedBy)
                .OnDelete (DeleteBehavior.Restrict);

            builder.Property (up => up.IsGranted)
                .IsRequired ();

            builder.Property (up => up.GrantedAt)
                .IsRequired ();

            builder.Property (up => up.IsActive)
                .HasDefaultValue (true);

        }
    }
}

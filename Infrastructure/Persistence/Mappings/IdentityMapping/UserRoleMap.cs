using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.RoleId);

            builder
                .HasKey(x => new
                {
                     x.UserId,
                     x.RoleId
                });
        }
    }
}

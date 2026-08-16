using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure ( EntityTypeBuilder<User> builder )
        {

            builder
                .HasMany (x => x.UserRole)
                .WithOne (x => x.User)
                .HasForeignKey (x => x.UserId)
                .OnDelete (DeleteBehavior.Cascade);

            builder
                .HasMany (x => x.RefreshToken)
                .WithOne (x => x.User)
                .HasForeignKey (x => x.UserId)
                .OnDelete (DeleteBehavior.Cascade);

            builder
                .HasMany (x => x.UserSession)
                .WithOne (x => x.User)
                .HasForeignKey (x => x.UserId)
                .OnDelete (DeleteBehavior.NoAction);

            builder
                .Property (u => u.Status)
                .HasConversion<string> ();

        }
    }
}

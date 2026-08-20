using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class UserSessionMap : IEntityTypeConfiguration<UserSession>
    {
        public void Configure ( EntityTypeBuilder<UserSession> builder )
        {
            builder
                .HasOne (x => x.User)
                .WithMany (x => x.UserSession)
                .HasForeignKey (x => x.UserId)
                .OnDelete (DeleteBehavior.NoAction);

            builder
                .HasOne (x => x.RefreshToken)
                .WithOne (x => x.UserSession)
                .HasForeignKey<RefreshToken> (x => x.UserSessionId)
                .OnDelete (DeleteBehavior.NoAction);
        }
    }
}

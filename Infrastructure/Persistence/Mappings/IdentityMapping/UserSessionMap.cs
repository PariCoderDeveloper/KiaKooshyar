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
    public class UserSessionMap : IEntityTypeConfiguration<UserSession>
    {
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.UserSession)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.RefreshToken)
                .WithMany()
                .HasForeignKey(x => x.RefreshTokenId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}

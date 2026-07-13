using KiaKooshar.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
               .HasOne(x => x.User)
               .WithMany(x => x.RefreshToken)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

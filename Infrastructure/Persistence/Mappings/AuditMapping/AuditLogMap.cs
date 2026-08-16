using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.AuditMapping
{
    public class AuditLogMap : IEntityTypeConfiguration
        <KiaKooshar.Domain.Entities.Audit.AuditLog>
    {
        public void Configure (
            EntityTypeBuilder<KiaKooshar.Domain.Entities.Audit.AuditLog> builder
            )
        {
            builder.HasKey (x => x.Id);

            builder.Property (x => x.TableName)
                .HasMaxLength (128)
                .IsRequired ();

            builder.Property (x => x.KeyValues)
                .HasMaxLength (512)
                .IsRequired ();

            builder.Property (x => x.OldValues)
                .HasColumnType ("nvarchar(max)");

            builder.Property (x => x.NewValues)
                .HasColumnType ("nvarchar(max)");

            builder.Property (x => x.ChangeType)
                .HasMaxLength (20)
                .IsRequired ();

            builder.Property (x => x.ChangedColumns)
                .HasColumnType ("nvarchar(max)");

            builder.Property (x => x.Username)
                .HasMaxLength (256);

            builder.Property (x => x.IP)
                .HasMaxLength (45);

            builder.HasIndex (x => x.UserId);

            builder.HasIndex (x => x.TableName);

            builder.HasIndex (x => new { x.TableName, x.KeyValues });
        }
    }
}
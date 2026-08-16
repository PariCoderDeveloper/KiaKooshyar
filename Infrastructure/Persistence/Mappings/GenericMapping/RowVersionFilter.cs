using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.GenericMapping
{
    public static class RowVersionFilter
    {
        public static void ApplyRowVersionConcurrencyToken (
            this ModelBuilder modelBuilder
            )
        {
            foreach ( var entityType in modelBuilder.Model.GetEntityTypes () )
            {
                if ( !typeof (BaseEntity).IsAssignableFrom (entityType.ClrType) )
                    continue;
                modelBuilder.Entity (entityType.ClrType)
                    .Property (nameof (BaseEntity.RowVersion))
                    .IsRowVersion ();
            }
        }
    }
}

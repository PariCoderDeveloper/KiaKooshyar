using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KiaKooshar.Infrastructure.Persistence.Mappings.IdentityMapping.GenericMapping
{
    public static class SoftDeleteFilter
    {
        public static void ApplySoftDeleteQueryFilter ( this ModelBuilder modelBuilder )
        {
            foreach ( var entityType in modelBuilder.Model.GetEntityTypes () )
            {
                if ( !typeof (BaseEntity).IsAssignableFrom (entityType.ClrType) )
                    continue;

                var method = typeof (SoftDeleteFilter)
                    .GetMethod (nameof (SetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod (entityType.ClrType);
                method.Invoke (null, new object[] { modelBuilder });
            }
        }
        private static void SetSoftDeleteFilter<TEntity> ( ModelBuilder modelBuilder )
            where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity> ()
                .HasQueryFilter (x => !x.IsDeleted);
        }
    }
}

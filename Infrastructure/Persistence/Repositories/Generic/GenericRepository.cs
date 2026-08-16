using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository ( DatabaseContext context )
        {
            _context = context;
            _dbSet = context.Set<T> ();
        }

        public virtual async Task<T> GetByIdAsync (
            long id,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return await _dbSet
                .Where (e => e.Id == id)
                .FirstOrDefaultAsync ();
        }
        public virtual async Task<List<T>> GetAllAsync (
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return await _dbSet
                .AsNoTracking ()
                .ToListAsync (cancellationToken);
        }
        public virtual async Task<List<T>> GetAllAsync (
            Expression<Func<T, bool>> wherePredicate,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return await _dbSet
                .AsNoTracking ()
                .Where (wherePredicate)
                .ToListAsync ();
        }
        public virtual async Task<T> AddAsync (
            T entity,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var addedEntity = await _dbSet.AddAsync (entity);
            return addedEntity.Entity;
        }
        public virtual void Delete<T> (
            T entity
            ) where T : BaseEntity
        {
            entity.IsDeleted = true;
            _context.Entry (entity).State = EntityState.Modified;
        }
    }
}

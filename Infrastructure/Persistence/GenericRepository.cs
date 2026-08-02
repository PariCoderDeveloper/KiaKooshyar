using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.Persistence
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

        public virtual async Task<TResult> GetByIdAsync<TResult> (
            Expression<Func<T, TResult>>? selector,
            long id,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            if ( selector == null )
                throw new ArgumentNullException (nameof (selector));
            return await _dbSet
                .Where (e => e.Id == id)
                .Select (selector)
                .FirstOrDefaultAsync ();
        }
        public virtual async Task<List<TResult>> GetAllAsync<TResult> (
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return await _dbSet.AsNoTracking ()
                .Select (selector).ToListAsync ();
        }
        public virtual async Task<List<TResult>> GetAllAsync<TResult> (
            Expression<Func<T, bool>> wherePredicate,
            Expression<Func<T, TResult>> selectExpression,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return await _dbSet
                .AsNoTracking ()
                .Where (wherePredicate)
                .Select (selectExpression)
                .ToListAsync ();
        }

        public virtual async Task AddAsync (
            T entity,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            await _dbSet.AddAsync (entity);
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

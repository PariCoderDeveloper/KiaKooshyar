using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.BaseEntities;
using KiaKooshar.Infrastructure.Persistence.Specification;
using Microsoft.EntityFrameworkCore;

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

        public void AddAsync (
            T entity
            )
        {
            _dbSet.AddAsync (entity);
        }
        public async Task<List<T>> ListAsync (
            ISpecifications<T> specifications,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var query = SpecificationEvaluator.GetQuery (
                    _dbSet.AsNoTracking (),
                    specifications
                );
            return await query.ToListAsync ();
        }

        public async Task<T> FirstOrDefaultAsync (
            ISpecifications<T> specifications,
            CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var query = SpecificationEvaluator.GetQuery (
                _dbSet.AsQueryable (),
                specifications);

            return await query.FirstOrDefaultAsync ();
        }
        public async Task<int> CountAsync (
                ISpecifications<T> specifications,
                CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var query = SpecificationEvaluator.GetQuery (
                _dbSet.AsQueryable (),
                specifications);
            return await query.CountAsync ();
        }

        public async Task<bool> AnyAsync (
            ISpecifications<T> specifications
            , CancellationToken cancellationToken
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            var query = SpecificationEvaluator.GetQuery (
                _dbSet.AsQueryable (),
                specifications);
            return await query.AnyAsync ();
        }
        public void Delete ( T entity )
        {
            entity.IsDeleted = true;
        }
    }
}

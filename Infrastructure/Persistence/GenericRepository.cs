using KiaKooshar.Application.Construct.DataBases;
using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.Persistence
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void AddAsync(T entity) => _dbSet.AddAsync(entity);

        public async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<T, TResult>> selector
            )
            => await _dbSet.AsNoTracking().Select(selector).ToListAsync();

        public async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<TResult, bool>> wherePeredict,
            Expression<Func<T, TResult>> selectExperssion
            ) => await _dbSet.AsNoTracking().Select(selectExperssion).Where(wherePeredict).ToListAsync();

        public async Task<T?> GetByIdAsync(long id) => await _dbSet.FindAsync(id);

        public async Task<TResult?> GetByIdAsync<TResult>(
            Expression<Func<T, TResult?>> selector,
            long id
            ) => await _dbSet.Select(selector).FirstOrDefaultAsync();

        public void Delete<T>(T entity) where T : BaseEntity
        {
            entity.IsDeleted = true;
        }
    }
}

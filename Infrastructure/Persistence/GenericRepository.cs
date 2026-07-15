using KiaKooshar.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KiaKooshar.Application.Construct.DataBases;

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
            => await _dbSet.Select(selector).ToListAsync();

        public async Task<List<TResult>> GetAllAsync<TResult>(
            Expression<Func<TResult, bool>> wherePeredict,
            Expression<Func<T, TResult>> selectExperssion
            ) => await _dbSet.Select(selectExperssion).Where(wherePeredict).ToListAsync();

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

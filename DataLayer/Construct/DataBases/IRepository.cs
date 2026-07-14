using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id);
        Task<TResult> GetByIdAsync<TResult>(Expression<Func<T, TResult>> selector,long id);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector);
        Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> wherePeredict,
            Expression<Func<T, TResult>> selectExperssion);
        void AddAsync(T entity);
        void Delete<T>(T entity) where T : BaseEntity;

     }
}

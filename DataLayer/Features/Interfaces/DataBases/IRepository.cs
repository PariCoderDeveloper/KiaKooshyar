using KiaKooshar.Domain.Entities.BaseEntities;
using System.Linq.Expressions;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<TResult?> GetByIdAsync<TResult> (
            Expression<Func<T, TResult>>? selector,
            long id,
            CancellationToken cancellationToken = default
        );
        Task<List<TResult>> GetAllAsync<TResult> (
            Expression<Func<T, TResult>> selector,
            CancellationToken cancellationToken = default
        );
        Task<List<TResult>> GetAllAsync<TResult> (
            Expression<Func<T, bool>> wherePredicate,
            Expression<Func<T, TResult>> selectExpression,
            CancellationToken cancellationToken = default
        );
        Task AddAsync (
            T entity,
            CancellationToken cancellationToken = default
        );
        void Delete<T> (
            T entity
        ) where T : BaseEntity;
    }
}

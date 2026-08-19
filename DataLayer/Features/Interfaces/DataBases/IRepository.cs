using KiaKooshar.Application.Common.Models;
using KiaKooshar.Domain.Entities.BaseEntities;
using System.Linq.Expressions;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<T?> GetByIdAsync (
            long id,
            CancellationToken cancellationToken = default
        );
        Task<List<T>> GetAllAsync (
            CancellationToken cancellationToken = default
        );
        Task<List<T>> GetAllAsync (
                Expression<Func<T, bool>> wherePredicate,
            CancellationToken cancellationToken = default
        );
        Task<T> AddAsync (
            T entity,
            CancellationToken cancellationToken = default
        );
        void Delete<T> (
            T entity
        ) where T : BaseEntity;

        Task<PagedResult<T>> GetPagedAsync (
          PaginationRequest request,
          Expression<Func<T, bool>>? filter = null,
          CancellationToken cancellationToken = default
          );
    }
}

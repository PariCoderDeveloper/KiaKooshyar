using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        void Add ( T entity );
        Task<List<T>> ListAsync (
            Specification<T> specifications,
            CancellationToken cancellationToken );
        Task<T?> FirstOrDefaultAsync (
            Specification<T?> specifications,
            CancellationToken cancellationToken );

        Task<int> CountAsync (
            Specification<T> specifications,
            CancellationToken cancellationToken
            );
        Task<bool> AnyAsync (
            Specification<T> specifications,
            CancellationToken cancellationToken
            );
        void Delete ( T entity );
    }
}

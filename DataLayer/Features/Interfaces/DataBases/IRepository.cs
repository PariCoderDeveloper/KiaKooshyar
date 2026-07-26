using KiaKooshar.Application.Specifications.Base;
using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Application.Construct.DataBases
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        void AddAsync ( T entity );
        Task<List<T>> ListAsync (
            ISpecifications<T> specifications,
            CancellationToken cancellationToken );
        Task<T?> FirstOrDefaultAsync (
            ISpecifications<T?> specifications,
            CancellationToken cancellationToken );

        Task<int> CountAsync (
            ISpecifications<T> specifications,
            CancellationToken cancellationToken
            );
        Task<bool> AnyAsync (
            ISpecifications<T> specifications,
            CancellationToken cancellationToken
            );
        void Delete ( T entity );
    }
}

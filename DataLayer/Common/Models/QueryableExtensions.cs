using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace KiaKooshar.Application.Common.Models
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>>
            ToPagedResultAsync<T> (
                this IQueryable<T> query,
                PaginationRequest request,
                CancellationToken cancellationToken = default )
        {
            if ( !string.IsNullOrWhiteSpace (request.SortBy) )
            {
                var direction = request.SortDescending
                    ? "descending"
                    : "ascending";
                query = query.OrderBy (
                    $"{request.SortBy} {direction}"
                    );
            }

            var totalCount = await query.CountAsync
                (cancellationToken);

            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            var items = await query
                .Skip ((pageNumber - 1) * pageSize)
                .Take (pageSize)
                .ToListAsync (cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

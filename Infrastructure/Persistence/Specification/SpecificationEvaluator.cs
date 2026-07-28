using KiaKooshar.Application.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace KiaKooshar.Infrastructure.Persistence.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T> (
                IQueryable<T> inputQuery,
                ISpecifications<T> specifications
            ) where T : class
        {
            var query = inputQuery;

            if ( specifications.Criteria != null )
            {
                query = query.Where (specifications.Criteria);
            }

            foreach ( var include in specifications.Includes )
            {
                query = query.Include (include);
            }
            foreach ( var includeString in specifications.IncludeStrings )
            {
                query = query.Include (includeString);
            }

            if ( specifications.OrderBy != null )
            {
                query = query.OrderBy (specifications.OrderBy);
            }
            else if ( specifications.OrderByDescending != null )
            {
                query = query.OrderByDescending (
                    specifications.OrderByDescending);
            }

            if ( specifications.IsPagingEnabled )
            {
                query = query
                    .Skip (specifications.Skip)
                    .Take (specifications.Take);
            }

            return query;
        }
    }
}

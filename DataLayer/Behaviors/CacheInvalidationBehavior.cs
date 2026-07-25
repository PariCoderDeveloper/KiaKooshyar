using KiaKooshar.Application.Caching.Contracts;
using MediatR;

namespace KiaKooshar.Application.Behaviors
{
    public class CacheInvalidationBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cache;
        public CacheInvalidationBehavior (
            ICacheService cache
            )
        {
            _cache = cache;
        }
        public async Task<TResponse> Handle (
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            var response = await next ();
            if ( request is ICacheInvalidationRequest cacheRequest )
            {
                foreach ( var key in cacheRequest.CacheKeys )
                {
                    await _cache.RemoveAsync (
                        key,
                        cancellationToken
                        );
                }
                if ( cacheRequest.CacheGroups != null )
                {
                    foreach ( var group in cacheRequest.CacheGroups )
                    {
                        await _cache.RemoveByPrefixAsync (
                            group,
                            cancellationToken
                            );
                    }
                }
            }
            return response;
        }
    }
}

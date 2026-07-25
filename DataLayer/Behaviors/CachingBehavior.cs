using KiaKooshar.Application.Caching.Contracts;
using MediatR;

namespace KiaKooshar.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> :
         IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cache;
        public CachingBehavior (
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
            if ( request is not ICacheableRequest cacheRequest )
                return await next ();

            var cachedResponse = await _cache.GetAsync<TResponse>
                (
                cacheRequest.CacheKey,
                cancellationToken
                );

            if ( cachedResponse is not null )
                return cachedResponse;

            var response = await next ();

            await _cache.SetAsync (
                cacheRequest.CacheKey,
                response,
                cacheRequest.Expiration,
                cancellationToken
                );
            return response;
        }
    }
}

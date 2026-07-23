namespace KiaKooshar.Application.Caching.Policies
{
    public sealed record CacheExpiration
        (
             TimeSpan AbsoluteExpiration,
             TimeSpan? SlidingExpiration = null
        );
}

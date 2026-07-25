namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ICacheInvalidationRequest
    {
        public string[] CacheKeys { get; }
        public string[]? CacheGroups { get; }
    }
}

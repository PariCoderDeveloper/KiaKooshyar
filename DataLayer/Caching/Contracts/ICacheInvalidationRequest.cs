namespace KiaKooshar.Application.Caching.Contracts
{
    public interface ICacheInvalidationRequest
    {
        public string[] CacheKeysList { get; }
    }
}

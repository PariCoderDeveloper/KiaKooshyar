namespace KiaKooshar.Application.Caching
{
    public class ICachable
    {
        public bool BypassCache { get; set; }
        public string CacheKey { get; set; } = null!;
        public TimeSpan? Expiration { get; set; }
    }
}
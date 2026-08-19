namespace KiaKooshar.Infrastructure.Caching.Options
{
    public class CacheSettings
    {
        public const string SectionName = "CacheSettings";
        public string Provider { get; set; } = "Memory";
    }
}

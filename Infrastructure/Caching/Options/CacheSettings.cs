namespace KiaKooshar.Infrastructure.Caching.Options
{
    public class CacheSettings
    {
        public const string SectionName = "CacheSettings";
        public string Provider { get; set; } = "Memory";
        public RedisSettings Redis { get; set; } = new ();
        public MemorySettings MemorySettings { get; set; } = new ();
    }
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = "localhost:6379";
        public string InstanceName { get; set; } = "KiaKooshyar";
    }
    public class MemorySettings
    {
        public long? SizeLimit { get; set; }
    }
}

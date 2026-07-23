namespace KiaKooshar.Application.Caching.Policies
{
    public static class CachePolicy
    {
        public static readonly CacheExpiration Short =
            new (
                TimeSpan.FromMinutes (5)
            );


        public static readonly CacheExpiration Medium =
            new (
                TimeSpan.FromMinutes (30),
                TimeSpan.FromMinutes (5)
            );


        public static readonly CacheExpiration Long =
            new (
                TimeSpan.FromHours (2),
                TimeSpan.FromMinutes (20)
            );
    }
}

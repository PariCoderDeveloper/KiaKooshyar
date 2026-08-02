namespace KiaKooshar.Application.Caching.Policies
{
    public class CacheKeys
    {
        public static string User ( long userId )
          => $"identity:user:{userId}";
    }
}

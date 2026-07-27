namespace KiaKooshar.Application.Caching.Policies
{
    public class CacheKeys
    {
        public static string UserPermissions ( long userId )
            => $"identity:user:{userId}:permissions";
    }
}

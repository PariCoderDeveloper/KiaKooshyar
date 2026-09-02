namespace KiaKooshar.Application.Features.Interfaces.Cache
{
    public interface IUserCacheSeeder
    {
        Task SeedToCacheAsync (
            CancellationToken cancellationToken = default
            );
    }
}

namespace KiaKooshar.Application.Features.Interfaces.Polly
{
    public interface IRedisResilienceService
    {
        Task<T> ExecuteAsync<T> (
            Func<CancellationToken, Task<T>> operation,
            CancellationToken cancellationToken = default
            );
        Task ExecuteAsync (
            Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken = default
           );
    }
}

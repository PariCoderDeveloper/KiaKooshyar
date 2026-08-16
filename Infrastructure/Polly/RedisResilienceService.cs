using KiaKooshar.Application.Features.Interfaces.Polly;
using Polly;
using Polly.Retry;

namespace KiaKooshar.Infrastructure.Polly
{
    public class RedisResilienceService :
        IRedisResilienceService
    {
        private readonly ResiliencePipeline _pipeline;
        public RedisResilienceService ()
        {
            _pipeline = new ResiliencePipelineBuilder ()
                .AddRetry (new RetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromMilliseconds (200),
                    BackoffType = DelayBackoffType.Exponential,
                    UseJitter = true
                })
                .AddTimeout (TimeSpan.FromSeconds (3))
                .Build ();
        }
        public async Task<T> ExecuteAsync<T> (
            Func<CancellationToken, Task<T>> operation,
            CancellationToken cancellationToken = default
            )
        {
            return await _pipeline.ExecuteAsync (
                async token =>
                    {
                        return await operation (token);
                    },
                    cancellationToken
                );
        }
        public async Task ExecuteAsync (
            Func<CancellationToken, Task> operation,
            CancellationToken cancellationToken = default
            )
        {
            await _pipeline.ExecuteAsync (
                async token =>
                    {
                        await operation (token);
                    },
                    cancellationToken
                );
        }
    }
}
using KiaKooshar.Application.Features.Interfaces.Jobs;
using KiaKooshar.Application.Features.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace KiaKooshar.Infrastructure.BackgroundJobs.JobScheduler
{
    public static class RecurringJobsRegistration
    {
        public static void RegisterRecurringJobs ( this IServiceProvider serviceProvider )
        {
            var scheduler = serviceProvider.GetRequiredService<IBackgroundJobScheduler> ();

            RegisterRefreshTokenCleanupJob (scheduler, serviceProvider);
        }

        private static void RegisterRefreshTokenCleanupJob (
            IBackgroundJobScheduler scheduler,
            IServiceProvider serviceProvider
            )
        {
            var job = serviceProvider.GetRequiredService<RefreshTokenCleanupJob> ();

            scheduler.AddOrUpdateRecurring<RefreshTokenCleanupJob> (
                job.JobId,
                x => x.ExecuteAsync (CancellationToken.None),
                job.CronExpression
            );
        }
    }
}
using KiaKooshar.Infrastructure.BackgroundJobs.JobScheduler;
using Microsoft.Extensions.DependencyInjection;

namespace KiaKooshar.Infrastructure.BackgroundJobs.JobSchaduler
{
    public static class AppRegisteregJob
    {
        public static async Task CleanupRefreshToken (
            this IServiceProvider provider
            )
        {
            using ( var scope = provider.CreateScope () )
            {
                scope.ServiceProvider.RegisterRecurringJobs ();
            }
        }

    }
}

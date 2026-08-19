using Hangfire;
using KiaKooshar.Application.Features.Interfaces.Jobs;
using System.Linq.Expressions;

namespace KiaKooshar.Infrastructure.BackgroundJobs
{
    public class HangfireJobScheduler : IBackgroundJobScheduler
    {
        public void Enqueue<T> ( Expression<Action<T>> methodCall )
            => BackgroundJob.Enqueue (methodCall);

        public void Enqueue<T> ( Expression<Func<T, Task>> methodCall )
            => BackgroundJob.Enqueue (methodCall);

        public void Schedule<T> (
            Expression<Action<T>> methodCall,
            TimeSpan delay
            )
            => BackgroundJob.Schedule (methodCall, delay);

        public void Schedule<T> (
            Expression<Func<T, Task>> methodCall,
            TimeSpan delay
            )
            => BackgroundJob.Schedule (methodCall, delay);

        public void AddOrUpdateRecurring<T> (
            string jobId,
            Expression<Action<T>> methodCall,
            string cronExpression
            )
            => RecurringJob.AddOrUpdate (jobId, methodCall, cronExpression);

        public void AddOrUpdateRecurring<T> (
            string jobId,
            Expression<Func<T, Task>> methodCall,
            string cronExpression
            )
            => RecurringJob.AddOrUpdate (jobId, methodCall, cronExpression);

        public void RemoveRecurring ( string jobId )
            => RecurringJob.RemoveIfExists (jobId);
    }
}
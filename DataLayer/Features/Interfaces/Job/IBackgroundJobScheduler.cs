using System.Linq.Expressions;

namespace KiaKooshar.Application.Features.Interfaces.Jobs
{
    public interface IBackgroundJobScheduler
    {
        void Enqueue<T> ( Expression<Action<T>> methodCall );
        void Enqueue<T> ( Expression<Func<T, Task>> methodCall );
        void Schedule<T> ( Expression<Action<T>> methodCall, TimeSpan delay );
        void Schedule<T> ( Expression<Func<T, Task>> methodCall, TimeSpan delay );
        void AddOrUpdateRecurring<T> (
            string jobId,
            Expression<Action<T>> methodCall,
            string cronExpression
            );
        void AddOrUpdateRecurring<T> (
            string jobId,
            Expression<Func<T, Task>> methodCall,
            string cronExpression
            );
        void RemoveRecurring ( string jobId );
    }
}
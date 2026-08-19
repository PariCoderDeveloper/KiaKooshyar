namespace KiaKooshar.Application.Features.Interfaces.Job
{
    public interface IRecurringJob
    {
        string JobId { get; }
        string CronExpression { get; }
        Task ExecuteAsync ( CancellationToken cancellationToken = default );
    }
}

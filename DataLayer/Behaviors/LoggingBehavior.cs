using MediatR;
using Serilog;

namespace KiaKooshar.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle (
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            var requestName = typeof (TRequest).Name;
            Log.Information (
                "Handling request {requestName} with data {@Request}",
                requestName,
                request
                );
            var response = await next ();
            Log.Information (
                "Finished handling request {RequestName}",
                requestName);

            return response;
        }
    }
}

using KiaKooshar.Application.Features.Construct.Logging;
using KiaKooshar.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KiaKooshar.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IBaseLogger _baseLogger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoggingBehavior (
            IBaseLogger baseLogger,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _baseLogger = baseLogger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<TResponse> Handle (
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            var requestName = typeof (TRequest).Name;
            var actionName = typeof (TResponse).Name;

            _baseLogger.Logging (
                new DTOs.Commons.LogOptionsDTO
                {
                    Message = "Handling request {RequestName}: {@Request}",
                    Args = new object[]
                    {
                        requestName,
                        request
                    },
                    Level = LogLevel.Information,
                    IP = _httpContextAccessor.HttpContext?
                        .Connection
                        .RemoteIpAddress?
                        .ToString ()
                }
                );

            var response = await next ();

            _baseLogger.Logging (
                new DTOs.Commons.LogOptionsDTO
                {
                    Message = "Handling response {@Response}",
                    Args = new object[]
                    {
                       response
                    },
                    Level = LogLevel.Information,
                    IP = _httpContextAccessor.HttpContext?
                         .Connection
                         .RemoteIpAddress?
                         .ToString ()
                });

            return response;
        }
    }
}

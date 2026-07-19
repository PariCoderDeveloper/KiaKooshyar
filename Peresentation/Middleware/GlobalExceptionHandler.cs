using KiaKooshar.Application.DTOs.Common;
using System.Text.Json;

namespace KiaKooshar.Peresentation.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler (
            RequestDelegate next,
            ILogger<GlobalExceptionHandler> logger
            )
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync ( HttpContext context )
        {
            try
            {
                await _next (context);
            }
            catch ( Exception ex )
            {
                // await HandleExceptionAsync (context, ex);
                _logger.LogError (ex, ex.Message);

                throw;

            }
        }

        private async Task HandleExceptionAsync ( HttpContext context, Exception exception )
        {
            var errorId = Guid.NewGuid ().ToString ("N");


            var result = ResultDTO.ServerError (
                 "An unexpected server error occurred.",
                 new List<string>
                 {
                    $"ErrorId: {errorId}",
                    $"Type: {exception.GetType().Name}" ,
                    $"message: {exception.Message}",
             });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync (
                JsonSerializer.Serialize (result)
            );

        }
    }
}

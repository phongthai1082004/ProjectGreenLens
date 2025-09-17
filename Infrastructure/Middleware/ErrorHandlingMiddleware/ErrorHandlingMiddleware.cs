using ProjectGreenLens.Settings;
using System.Text.Json;

namespace ProjectGreenLens.Infrastructure.Middleware.ErrorHandlingMiddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                // map exception -> (statusCode, message)
                var (statusCode, message, logLevel) = MapException(ex);

                // log theo severity
                _logger.Log(logLevel, ex, "Exception caught: {Message}", ex.Message);

                // tạo error response
                var errorResponse = ApiResponse<object>.Fail(message);

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsync(json);
            }
        }

        private static (int StatusCode, string Message, LogLevel Level) MapException(System.Exception ex)
        {
            return ex switch
            {
                ProjectGreenLens.Exceptions.NotFoundException => (StatusCodes.Status404NotFound, ex.Message, LogLevel.Warning),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, ex.Message, LogLevel.Warning),
                InvalidOperationException => (StatusCodes.Status400BadRequest, ex.Message, LogLevel.Warning),
                System.ComponentModel.DataAnnotations.ValidationException => (StatusCodes.Status400BadRequest, ex.Message, LogLevel.Warning),
                ProjectGreenLens.Exceptions.ValidationException => (StatusCodes.Status400BadRequest, ex.Message, LogLevel.Warning),

                _ => (StatusCodes.Status500InternalServerError, "Internal server error", LogLevel.Error)
            };
        }
    }
}

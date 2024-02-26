namespace api_sylvainbreton.Exceptions
{
    using Microsoft.AspNetCore.Http;
    using System.Net;
    using System.Text.Json;

    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError; // Default
            var message = "An unexpected error occurred.";

            // Map exceptions to specific status codes
            if (ex is BadRequestException) statusCode = HttpStatusCode.BadRequest;
            else if (ex is NotFoundException) statusCode = HttpStatusCode.NotFound;
            else if (ex is InternalServerErrorException) message = ex.Message; // Keep the default status code

            // You can customize logic to extract error messages if needed
            if (ex is BadRequestException badRequestEx) message = badRequestEx.Message;
            else if (ex is NotFoundException notFoundEx) message = notFoundEx.Message;

            _logger.LogError(ex, "Exception middleware caught the following exception: {ExceptionMessage}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                statusCode = context.Response.StatusCode,
                message
            }));
        }
    }
}

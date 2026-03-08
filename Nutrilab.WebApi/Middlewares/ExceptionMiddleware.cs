using Nutrilab.Shared.Models.Exceptions;
using System.Text.Json;

namespace Nutrilab.WebApi.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                NotFoundException e => (StatusCodes.Status404NotFound, e.Message),
                UnauthorizedException e => (StatusCodes.Status401Unauthorized, e.Message),
                BadRequestException e => (StatusCodes.Status400BadRequest, e.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = JsonSerializer.Serialize(new { message, status = statusCode });
            return context.Response.WriteAsync(response);
        }
    }
}

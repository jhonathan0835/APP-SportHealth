using APP_SportHealth.API.Responses;
using APP_SportHealth.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace APP_SportHealth.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex) // 🔥 NUEVO
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                await HandleValidationException(context, errors);
            }
            catch (BusinessException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                await HandleException(context, "Error interno del servidor", HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleException(HttpContext context, string message, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }

        private async Task HandleValidationException(HttpContext context, List<string> errors)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            var response = new
            {
                success = false,
                message = "Errores de validación",
                errors = errors
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }


    }
}

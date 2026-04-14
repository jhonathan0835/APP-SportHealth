using APP_SportHealth.API.Responses;
using APP_SportHealth.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace APP_SportHealth.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                Log.Warning("Error de validación: {@Errors}", ex.Errors);
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                await HandleValidationException(context, errors);
            }
            catch (BusinessException ex)
            {
                Log.Warning("Error de negocio: {Message}", ex.Message);
                await HandleException(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error interno del servidor");
                await HandleException(context, "Error interno del servidor", HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleException(HttpContext context, string message, HttpStatusCode statusCode, Exception? exception = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            // In development include exception details to help debugging
            var response = new ApiResponse<object>
            {
                Success = false,
                Message = message,
                Data = null
            };

            if (_env != null && _env.EnvironmentName == "Development" && exception != null)
            {
                var details = new { error = exception.Message, stackTrace = exception.StackTrace };
                // attach details in Data for debugging (only in Development)
                response = new ApiResponse<object>
                {
                    Success = false,
                    Message = message,
                    Data = details
                };
            }

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

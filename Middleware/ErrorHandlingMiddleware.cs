using System.Net;
using System.Text.Json;
using LibrarySystem.Exceptions;

namespace LibrarySystem.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next) { _next = next; }

        public async Task Invoke(HttpContext context)
        {
            try { await _next(context); }
            catch (NotFoundException ex) { await WriteError(context, HttpStatusCode.NotFound, ex.Message); }
            catch (BusinessRuleException ex) { await WriteError(context, HttpStatusCode.BadRequest, ex.Message); }
            catch (Exception ex) { await WriteError(context, HttpStatusCode.InternalServerError, $"Wystąpił nieoczekiwany błąd: {ex.Message}"); }
        }

        private static async Task WriteError(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = (int)statusCode;
            var payload = JsonSerializer.Serialize(new { status = (int)statusCode, message });
            await context.Response.WriteAsync(payload);
        }
    }
}

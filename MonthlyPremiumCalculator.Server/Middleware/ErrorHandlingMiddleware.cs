//namespace MonthlyPremiumCalculatorAPI.Middleware
// Middleware/ErrorHandlingMiddleware.cs
using System.Net;
using System.Text.Json;

namespace MonthlyPremiumCalculatorAPI.Middleware
{

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next; _logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try { await _next(ctx); }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Validation error");
                ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled error");
                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Internal server error" }));
            }
        }
    }
}

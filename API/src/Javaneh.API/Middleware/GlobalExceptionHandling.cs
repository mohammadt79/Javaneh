
using System.Net;
using System.Text.Json;

namespace Javaneh.API.Middleware;

public class GlobalExceptionHandling
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandling> _logger;

    public GlobalExceptionHandling(
        RequestDelegate next,
        ILogger<GlobalExceptionHandling> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            await HandleExceptionAsync(context);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode =
            (int)HttpStatusCode.InternalServerError;

        context.Response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new
        {
            statusCode = context.Response.StatusCode,
            error = "An internal server error occurred."
        });

        return context.Response.WriteAsync(result);
    }
}

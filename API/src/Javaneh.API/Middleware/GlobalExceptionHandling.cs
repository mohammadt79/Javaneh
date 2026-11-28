using System.Net;
using Javaneh.API.Contracts.Responses;
using Javaneh.API.Helpers;

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

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, _) = exception switch
        {
            KeyNotFoundException =>
                (HttpStatusCode.NotFound, ""),

            UnauthorizedAccessException =>
                (HttpStatusCode.Forbidden, ""),

            ArgumentException =>
                (HttpStatusCode.BadRequest, ""),

            _ =>
                (HttpStatusCode.InternalServerError, "")
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<object>
        {
            Code = (int)statusCode,
            Message = ResponseMessageHelper.GetMessage((int)statusCode),
            Data = null
        };
        return context.Response.WriteAsJsonAsync(response);
    }
}
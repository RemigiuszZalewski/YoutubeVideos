using System.Net;
using ExceptionHandler.Exceptions;
using ExceptionHandler.Models;

namespace ExceptionHandler.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new Error
        {
            Message = exception.Message,
            ExceptionType = exception.GetType().Name,
            StatusCode = exception switch
            {
                PlayerNotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            },
            StackTrace = exception.StackTrace
        };
        
        _logger.LogError(error.ToString());

        context.Response.StatusCode = (int)error.StatusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}
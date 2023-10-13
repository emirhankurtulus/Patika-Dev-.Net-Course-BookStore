using Newtonsoft.Json;
using Project.Services;
using System.Diagnostics;
using System.Net;

namespace Project.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private ILoggerService _loggerService;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        _next = next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        try
        {
            var message = "[Request] Http " + context.Request.Method + " - " + context.Request.Path;
            _loggerService.Write(message);

            await _next(context);
            watch.Stop();

            message = "[Response] Http " + context.Request.Method + " - " + context.Request.Path + " " + context.Response.StatusCode + " responded in " + watch.Elapsed.Microseconds + " ms";
            _loggerService.Write(message);
        }
        catch (Exception ex)
        {
            watch.Stop();

            await HandleException(context, ex, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
    {
        var message = "[Error] HTTP" + context.Request.Method + " - " + context.Response.StatusCode + " Error Message:" + ex.Message + " in " + watch.Elapsed.Milliseconds + " ms";
        _loggerService.Write(message);

        context.Response.ContentType = "application/json";

        if(ex.Source == "FluentValidation")
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        }
        else
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}
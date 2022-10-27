using System.Net;
using System.Text.Json;
using DominoTrains.Domain.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;

namespace DominoTrains.Api.Middleware;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
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
        var (statusCode, title) = exception switch
        {
            ViolationException => (HttpStatusCode.BadRequest, "An incorrect action was taken."),
            NotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
            UnexpectedException or _ => (HttpStatusCode.InternalServerError, "Something went wrong."),
        };

        var problemDetails = new ProblemDetails
        {
            Title = title,
            Status = (int)statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(result);
    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(this IApplicationBuilder app) => app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}
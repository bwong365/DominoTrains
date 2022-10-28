using System.Net;
using System.Text.Json;
using DominoTrains.Domain.Exceptions.BaseExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FluentValidation;
using DominoTrains.Api.Utils;

namespace DominoTrains.Api.Middleware;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<GlobalErrorHandlerMiddleware> logger)
    {
        var (statusCode, title) = exception switch
        {
            ValidationException => (HttpStatusCode.BadRequest, "One or more validation errors occurred."),
            ViolationException => (HttpStatusCode.BadRequest, "An incorrect action was taken."),
            NotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found."),
            UnexpectedException or _ => (HttpStatusCode.InternalServerError, "Something went wrong."),
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError("Message: {Message}, StackTrace: {@StackTrace}", exception.Message, exception.StackTrace);
        }

        var result = exception switch
        {
            ValidationException validationException => JsonSerializer.Serialize<ValidationProblemDetails>(GetValidationProblemDetails(validationException)),
            _ => JsonSerializer.Serialize<ProblemDetails>(new ProblemDetails { Title = title, Status = (int)statusCode, Detail = exception.Message, Instance = context.Request.Path })
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(result);
    }

    private ValidationProblemDetails GetValidationProblemDetails(ValidationException exception)
    {
        var modelState = new ModelStateDictionary();
        foreach (var error in exception.Errors)
        {
            modelState.AddModelError(error.PropertyName.FromPascalToCamelCase(), error.ErrorMessage);
        }
        return new ValidationProblemDetails(modelState);
    }
}

public static partial class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandlerMiddleware(this IApplicationBuilder app) => app.UseMiddleware<GlobalErrorHandlerMiddleware>();
}
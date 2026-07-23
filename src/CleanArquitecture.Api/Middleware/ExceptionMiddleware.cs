using CleanArquitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArquitecture.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
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
      
            var exceptionDetails = ExceptionHandler(ex);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail
            };

            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ExceptionDetails ExceptionHandler(Exception exception)
    {

        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationError",
                "Validation Error",
                validationException.Errors),

            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "InternalServerError",
                "Internal Server Error",
                null)
        };

    }

    internal record ExceptionDetails(int Status, string Title, string Detail, IEnumerable<object>? Errors);
}

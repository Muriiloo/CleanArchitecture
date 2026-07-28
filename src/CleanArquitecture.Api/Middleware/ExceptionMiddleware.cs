using CleanArquitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

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

            InvalidOperationException invalidOperationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "Invalid Operation",
                "The request could not be processed in the resource's current state.",
                null),

            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred. Please try again later.",
                null)
        };

    }

    internal record ExceptionDetails(int Status, string Title, string Detail, IEnumerable<object>? Errors);
}

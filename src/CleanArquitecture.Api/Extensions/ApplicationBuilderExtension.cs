using CleanArquitecture.Api.Middleware;

namespace CleanArquitecture.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {

        app.UseMiddleware<ExceptionMiddleware>();

        return app;
    }
}

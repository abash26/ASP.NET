
namespace MiddlewareWebApp.MiddlewareComponents;

public class ExceptionMiddleware : IMiddleware
{
    private ILogger<ExceptionMiddleware> logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        this.logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            context.Response.ContentType = "text/html";
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the request.");
            await context.Response.WriteAsync($"<h5>Error: </h5>");
            await context.Response.WriteAsync($"<p>{ex.Message}</p>");
        }
    }
}


namespace MiddlewareWebApp.MiddlewareComponents;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            context.Response.ContentType = "text/html";
            await next(context);
        }
        catch (Exception ex)
        {
            await context.Response.WriteAsync($"<h5>Error: </h5>");
            await context.Response.WriteAsync($"<p>{ex.Message}</p>");
        }
    }
}

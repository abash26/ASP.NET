var builder = WebApplication.CreateBuilder(args); // starts kestrel server
var app = builder.Build(); // builds webApp

// app.MapGet("/", () => "Hello World!"); // middleware pipeline component, endpoint handler
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync($"The method is: {context.Request.Method}\r\n");
    await context.Response.WriteAsync($"The Url is: {context.Request.Path}\r\n");

    await context.Response.WriteAsync($"\r\nHeaders:\r\n");
    foreach (var key in context.Request.Headers.Keys)
    {
        await context.Response.WriteAsync($"{key}: {context.Request.Headers[key]}\r\n");
    }
});

app.Run(); // listens to port

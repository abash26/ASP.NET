var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/categories/{size=medium}", async (HttpContext context) =>
    {
        context.Response.WriteAsync($"Size {context.Request.RouteValues["size"]}");
    });

    endpoints.MapGet("/employees", async (HttpContext context) =>
    {
        context.Response.WriteAsync("Get employees");
    });

    endpoints.MapPost("/employees", async (HttpContext context) =>
    {
        context.Response.WriteAsync("Post employees");
    });

    endpoints.MapPut("/employees", async (HttpContext context) =>
    {
        context.Response.WriteAsync("Put employees");
    });

    endpoints.MapDelete("/employees/{id}", async (HttpContext context) =>
    {
        context.Response.WriteAsync($"Delete employee {context.Request.RouteValues["id"]}");
    });
});

app.Run();

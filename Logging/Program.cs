using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSerilog();

var app = builder.Build();


app.MapGet("/", (ILogger<Program> logger) =>
{
    logger.LogInformation("logging {userName} at {time}", "Frank", DateTime.Now);
    return "Hello World!";
});

app.Run();

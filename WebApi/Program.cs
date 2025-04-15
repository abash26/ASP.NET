using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi("v1", options =>
{
    options.ShouldInclude(new ApiDescription { GroupName = "v1" });
});
builder.Services.AddOpenApi("v2", options =>
{
    options.ShouldInclude(new ApiDescription { GroupName = "v2" });
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new QueryStringApiVersionReader("v");
})
.AddMvc()
.AddApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Employee API V1");
        options.SwaggerEndpoint("/openapi/v2.json", "Employee API V2");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Routing.Endpoints;
using Routing.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
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
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("v"),
        new HeaderApiVersionReader("x-api-version"),
        new MediaTypeApiVersionReader("version"));
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();
app.UseRouting();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .HasApiVersion(new ApiVersion(2))
    .ReportApiVersions()
    .Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseDeveloperExceptionPage();

app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Employee API V1");
    options.SwaggerEndpoint("/openapi/v2.json", "Employee API V2");
});


app.UseStatusCodePages();
app.MapEmployeeEndpoints(apiVersionSet);

app.Run();
using Routing.Endpoints;
using Routing.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();

var app = builder.Build();
app.UseRouting();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.MapEmployeeEndpoints();

app.Run();
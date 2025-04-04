using Microsoft.AspNetCore.Mvc;
using Routing.Models;
using Routing.Results;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseRouting();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.MapGet("/", HtmlResult () =>
{
    string html = "<h2>Welcome to our API</h2> Our API is used to learn ASP.NET CORE.";
    return new HtmlResult(html);
});

app.MapGet("/employees", () =>
{
    var employees = EmployeesRepository.GetEmployees();
    return TypedResults.Ok(employees);
});

app.MapGet("/employees/{id:int}", ([FromRoute(Name = "id")] int employeeId) =>
{
    var employee = EmployeesRepository.GetEmployeeById(employeeId);
    if (employee is null)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not found." } }
        }, statusCode: 404);
    }
    return TypedResults.Ok(employee);
});

app.MapPost("/employees", (Employee? employee) =>
{
    if (employee is null)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not provided or is not valid." } }
        }, statusCode: 404);
    }

    EmployeesRepository.AddEmployee(employee);
    return TypedResults.Created();
}).WithParameterValidation();

app.MapPut("/employees/{id:int}", (int id, [FromBody] Employee employee) =>
{
    if (id != employee.Id)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not provided or is not valid." } }
        }, statusCode: 400);
    }
    var result = EmployeesRepository.UpdateEmployee(employee);

    if (!result)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee not found." } }
        }, statusCode: 404);
    }

    return TypedResults.Ok("Employee updated successfully.");
}).WithParameterValidation();

app.MapDelete("/employees/{id:int}", (int id) =>
{
    var employee = EmployeesRepository.GetEmployeeById(id);
    var result = EmployeesRepository.DeleteEmployee(employee);

    if (!result)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee not found." } }
        }, statusCode: 404);
    }

    return TypedResults.Ok(employee);
});

app.Run();
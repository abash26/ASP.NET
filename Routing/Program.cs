using Microsoft.AspNetCore.Mvc;
using Routing.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseRouting();

app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Headers["Content-Type"] = "text/html";

    await context.Response.WriteAsync($"The method is: {context.Request.Method}<br/>");
    await context.Response.WriteAsync($"The Url is: {context.Request.Path}<br/>");
    await context.Response.WriteAsync($"<b>Headers</b>:<br/>");
    await context.Response.WriteAsync("<ul>");
    foreach (var key in context.Request.Headers.Keys)
    {
        await context.Response.WriteAsync($"<li><b>{key}</b>: {context.Request.Headers[key]}</li>");
    }
    await context.Response.WriteAsync("</ul>");
});

app.MapGet("/employees", async (HttpContext context) =>
{
    var employees = EmployeesRepository.GetEmployees();
    context.Response.StatusCode = 200;
    context.Response.Headers["Content-Type"] = "text/html";
    await context.Response.WriteAsync("<ul>");
    foreach (var employee in employees)
    {
        await context.Response.WriteAsync($"<li><b>{employee.Name}</b>: {employee.Position}</li>");
    }
    await context.Response.WriteAsync("</ul>");
});

app.MapGet("/employees/{id:int}", ([FromRoute(Name = "id")] int employeeId) =>
{
    var employee = EmployeesRepository.GetEmployeeById(employeeId);
    return employee;
});

app.MapPost("/employees", (Employee employee) =>
{
    if (employee is null || employee.Id <= 0)
    {
        return false;
    }

    EmployeesRepository.AddEmployee(employee);
    return true;
});

app.MapPut("/employees", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();
    var employee = JsonSerializer.Deserialize<Employee>(body);

    var result = EmployeesRepository.UpdateEmployee(employee);
    if (result)
    {
        context.Response.StatusCode = 201;
        await context.Response.WriteAsync("Employee updated successfully.");
        return;
    }
    else
    {
        await context.Response.WriteAsync("Employee not found.");
    }
});

app.MapDelete("/employees/{id}", async (HttpContext context) =>
{
    var id = context.Request.RouteValues["id"];
    if (int.TryParse(id?.ToString(), out int employeeId))
    {
        if (context.Request.Headers["Authorization"] == "frank")
        {
            var result = EmployeesRepository.DeleteEmployee(employeeId);

            if (result)
            {
                await context.Response.WriteAsync("Employee is deleted successfully.");
            }
            else
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Employee not found.");
            }
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("You are not authorized to delete.");
        }
    }
});

app.Run();
using Microsoft.AspNetCore.Mvc;
using Routing.Filters;
using Routing.Models;
using Routing.Results;

namespace Routing.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this WebApplication app)
    {
        app.MapGet("/", HtmlResult () =>
        {
            string html = "<h2>Welcome to our API</h2> Our API is used to learn ASP.NET CORE.";
            return new HtmlResult(html);
        });

        app.MapGet("/employees", (IEmployeesRepository employeesRepository) =>
        {
            var employees = employeesRepository.GetEmployees();
            return TypedResults.Ok(employees);
        });

        app.MapGet("/employees/{id:int}", (int id, IEmployeesRepository employeesRepository) =>
        {
            var employee = employeesRepository.GetEmployeeById(id);
            return TypedResults.Ok(employee);
        }).AddEndpointFilter<EmployeeExistsFilter>();

        app.MapPost("/employees", (Employee? employee, IEmployeesRepository employeesRepository) =>
        {
            employeesRepository.AddEmployee(employee);
            return TypedResults.Created();
        }).WithParameterValidation().AddEndpointFilter<EmployeeCreateFilter>();

        app.MapPut("/employees/{id:int}", (int id, [FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
        {
            employeesRepository.UpdateEmployee(employee);
            return TypedResults.Ok("Employee updated successfully.");
        })
        .WithParameterValidation()
        .AddEndpointFilter<EmployeeExistsFilter>()
        .AddEndpointFilter<EmployeeUpdateFilter>();

        app.MapDelete("/employees/{id:int}", (int id, IEmployeesRepository employeesRepository) =>
        {
            var employee = employeesRepository.GetEmployeeById(id);
            employeesRepository.DeleteEmployee(employee);

            return TypedResults.Ok(employee);
        }).AddEndpointFilter<EmployeeExistsFilter>();
    }
}

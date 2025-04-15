using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using Routing.Filters;
using Routing.Models;
using Routing.Results;

namespace Routing.Endpoints;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGet("/",
            [EndpointSummary("Get documents v1")]
        HtmlResult () =>
        {
            Console.WriteLine("Version 1.0");
            string html = "<h2>Welcome to our API</h2> Our API v1 is used to learn ASP.NET CORE.";
            return new HtmlResult(html);
        }).WithApiVersionSet(apiVersionSet).MapToApiVersion(new ApiVersion(1, 0));

        app.MapGet("/",
        [EndpointSummary("Get documents v2")]
        HtmlResult () =>
        {
            Console.WriteLine("Version 2.0");
            string html = "<h2>Welcome to our API</h2> Our API v2 is used to learn ASP.NET CORE.";
            return new HtmlResult(html);
        })
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(2, 0))
        .WithGroupName("v2");

        app.MapGet("/employees",

        [EndpointName("GetDepartments")]
        [EndpointSummary("Get documents")]
        [Tags("Web Api - Employees")]
        (IEmployeesRepository employeesRepository) =>
            {
                var employees = employeesRepository.GetEmployees();
                return TypedResults.Ok(employees);
            }).WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .MapToApiVersion(new ApiVersion(2, 0))
        .WithGroupName("v2");

        app.MapGet("/employees/{id:int}", (int id, IEmployeesRepository employeesRepository) =>
        {
            var employee = employeesRepository.GetEmployeeById(id);
            return TypedResults.Ok(employee);
        }).AddEndpointFilter<EmployeeExistsFilter>()
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .MapToApiVersion(new ApiVersion(2, 0))
        .WithGroupName("v2");

        app.MapPost("/employees", (Employee? employee, IEmployeesRepository employeesRepository) =>
        {
            employeesRepository.AddEmployee(employee);
            return TypedResults.Created();
        })
        .WithParameterValidation()
        .AddEndpointFilter<EmployeeCreateFilter>()
        .WithApiVersionSet(apiVersionSet) // Add this
        .MapToApiVersion(new ApiVersion(1, 0)) // Add this
        .MapToApiVersion(new ApiVersion(2, 0)) // Add this
        .WithGroupName("v2"); // Add this

        app.MapPut("/employees/{id:int}", (int id, [FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
        {
            employeesRepository.UpdateEmployee(employee);
            return TypedResults.Ok("Employee updated successfully.");
        })
        .WithParameterValidation()
        .AddEndpointFilter<EmployeeExistsFilter>()
        .AddEndpointFilter<EmployeeUpdateFilter>()
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .MapToApiVersion(new ApiVersion(2, 0))
        .WithGroupName("v1");

        app.MapDelete("/employees/{id:int}", (int id, IEmployeesRepository employeesRepository) =>
        {
            var employee = employeesRepository.GetEmployeeById(id);
            employeesRepository.DeleteEmployee(employee);

            return TypedResults.Ok(employee);
        }).AddEndpointFilter<EmployeeExistsFilter>()
        .WithApiVersionSet(apiVersionSet)
        .MapToApiVersion(new ApiVersion(1, 0))
        .MapToApiVersion(new ApiVersion(2, 0))
        .WithGroupName("v2");
    }
}

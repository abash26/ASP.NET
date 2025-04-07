using Microsoft.AspNetCore.Mvc;
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
            if (employee is null)
            {
                return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not found." } }
        }, statusCode: 404);
            }
            return TypedResults.Ok(employee);
        });

        app.MapPost("/employees", (Employee? employee, IEmployeesRepository employeesRepository) =>
        {
            if (employee is null)
            {
                return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not provided or is not valid." } }
        }, statusCode: 404);
            }

            employeesRepository.AddEmployee(employee);
            return TypedResults.Created();
        }).WithParameterValidation();

        app.MapPut("/employees/{id:int}", (int id, [FromBody] Employee employee, IEmployeesRepository employeesRepository) =>
        {
            if (id != employee.Id)
            {
                return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee is not provided or is not valid." } }
        }, statusCode: 400);
            }
            var result = employeesRepository.UpdateEmployee(employee);

            if (!result)
            {
                return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee not found." } }
        }, statusCode: 404);
            }

            return TypedResults.Ok("Employee updated successfully.");
        }).WithParameterValidation();

        app.MapDelete("/employees/{id:int}", (int id, IEmployeesRepository employeesRepository) =>
        {
            var employee = employeesRepository.GetEmployeeById(id);
            var result = employeesRepository.DeleteEmployee(employee);

            if (!result)
            {
                return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
        {
            {"id", new[] { "Employee not found." } }
        }, statusCode: 404);
            }

            return TypedResults.Ok(employee);
        });
    }
}


using Routing.Models;

namespace Routing.Filters;

public class EmployeeCreateFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var employee = context.GetArgument<Employee>(0);

        if (employee is null)
        {
            return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
            {
                {"id", new[] { "Employee is not provided or is not valid." } }
            }, statusCode: 404);
        }

        return await next(context);
    }
}

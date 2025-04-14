
using Routing.Models;

namespace Routing.Filters;

public class EmployeeExistsFilter(IEmployeesRepository employeesRepository) : IEndpointFilter
{
    private readonly IEmployeesRepository employeesRepository = employeesRepository;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.GetArgument<int>(0);

        if (!employeesRepository.EmployeeExists(id))
        {
            return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
            {
                { "id", new[] { "Employee is not found." } }
            }, statusCode: 404);
        }

        return await next(context);
    }
}

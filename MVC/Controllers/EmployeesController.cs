using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers;

public class EmployeesController : Controller
{
    // action method (endpoint handler)
    public IActionResult GetEmployeesByDepartment([FromRoute(Name = "id")] int department)
    {
        return Content($"Employees under department: {department}");
    }
}

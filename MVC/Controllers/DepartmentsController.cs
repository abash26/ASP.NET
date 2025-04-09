using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class DepartmentsController : Controller
{
    // action method (endpoint handler)
    public IActionResult Index()
    {
        return Content("<h2>Hello</h2>", "text/html");
    }

    public IActionResult Details(int? id)
    {
        return RedirectToAction("GetEmployeesByDepartment", "Employees", new { id = id });

        // return LocalRedirect($"/employees/GetEmployeesByDepartment/{id}");

        // return new RedirectResult("http://www.google.com");
        // return new LocalRedirectResult($"/employees/GetEmployeesByDepartment/{id}");
        // return new RedirectToActionResult("GetEmployeesByDepartment", "Employees", new { id = id });
        // return Json(new Department { Id = 1, Name = "test" });
    }

    [HttpPost("create")]
    public object Create([FromBody] Department department)
    {
        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return department;
    }

    [HttpDelete]
    public string Delete(int? id)
    {
        return $"Deleted department: {id}";
    }

    [HttpPut]
    public string Edit(int? id)
    {
        return $"Updated department: {id}";
    }

    [Route("/download/vf")]
    public IActionResult VirtualFile()
    {
        return File("/readme.txt", "text/plain");
    }

    [Route("/download/pf")]
    public IActionResult PhysicalFile()
    {
        return PhysicalFile(@"C:\temp\test.txt", "text/plain");
    }

    [Route("/download/cf")]
    public IActionResult ContentFile()
    {
        byte[] bytes = System.IO.File.ReadAllBytes(@"C:\temp\test.txt");

        return File(bytes, "text/plain");
    }
}

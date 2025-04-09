using Departments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Departments.Controllers;

public class DepartmentsController(IDepartmentsRepository departmentsRepository) : Controller
{
    private readonly IDepartmentsRepository _departmentsRepository = departmentsRepository;

    [HttpGet]
    public IActionResult Index()
    {
        var departments = _departmentsRepository.GetDepartments();
        var html = $@"
                    <h1>Departments</h1>
                    <ul>
                        {string.Join("", departments.Select(x => $@"
                                    <li>
                                        <a href='/departments/details/{x.Id}'>{x.Name} ({x.Description})</a>
                                    </li>
                                "))}
                    </ul>
                    <br/>
                    <a href='/departments/create'>Add Department</a>
                ";

        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var department = _departmentsRepository.GetDepartmentById(id);
        if (department == null)
        {
            return Content("<h3>Department not found</h3>", "text/html");
        }

        var html = $@"
                <h1>Department Details</h1>
                <form method='post' action='/departments/edit'>
                    <input type='hidden' name='Id' value='{department.Id}' />
                    <label>Name: <input type='text' name='Name' value='{department.Name}' /></label><br />
                    <label>Description: <input type='text' name='Description' value='{department.Description}' /></label><br />
                    <br/>
                    <a href='/departments'>Cancel</a>
                    <button type='submit'>Update</button>
                </form>

                <form method='post' action='/departments/delete/{department.Id}'>
                    <button type='submit' style='background-color:red;color:white'>Delete</button>
                </form>";

        return Content(html, "text/html");
    }

    [HttpPost]
    public IActionResult Edit(Department department)
    {
        if (!ModelState.IsValid)
        {
            return Content(GetErrorsHTML(), "text/html");
        }
        _departmentsRepository.UpdateDepartment(department);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        var html = @"
                <h1>Add Department</h1>
                <form method='post' action='/departments/create'>
                    <label>Name: <input type='text' name='Name' /></label><br />
                    <label>Description: <input type='text' name='Description' /></label><br />
                    <br/>
                    <button type='submit'>Add</button>
                </form>";

        return Content(html, "text/html");
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (!ModelState.IsValid)
        {
            return Content(GetErrorsHTML(), "text/html");
        }

        _departmentsRepository.AddDepartment(department);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var department = _departmentsRepository.GetDepartmentById(id);
        if (department == null)
        {
            ModelState.AddModelError("id", "Department not found.");

            return Content(GetErrorsHTML(), "text/html");
        }
        _departmentsRepository.DeleteDepartment(department);
        return RedirectToAction("Index");
    }

    private string GetErrorsHTML()
    {
        var errorMessages = new List<string>();
        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                errorMessages.Add(error.ErrorMessage);
            }
        }

        string html = string.Empty;
        if (errorMessages.Count > 0)
        {
            html = $@"
                    <ul>
                        {string.Join("", errorMessages.Select(error => $"<li style='color:red;'>{error}</li>"))}
                    </ul>";
        }

        return html;
    }
}

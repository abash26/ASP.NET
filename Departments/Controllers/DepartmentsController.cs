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
        return View(departments);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var department = _departmentsRepository.GetDepartmentById(id);
        if (department == null)
        {
            return View("Error", new List<string> { "Department not found" });
        }

        return View(department);
    }

    [HttpPost]
    public IActionResult Edit(Department department)
    {
        if (!ModelState.IsValid)
        {
            return View("Error", GetErrors());

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
            return View("Error", GetErrors());
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

            return View("Error", GetErrors());
        }
        _departmentsRepository.DeleteDepartment(department);
        return RedirectToAction("Index");
    }

    private List<string> GetErrors()
    {
        var errorMessages = new List<string>();
        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                errorMessages.Add(error.ErrorMessage);
            }
        }
        return errorMessages;
    }
}

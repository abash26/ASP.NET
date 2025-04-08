using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class DepartmentsController
{
    // action method (endpoint handler)
    public string Index()
    {
        return "These are the departments.";
    }

    public string Details(int? id)
    {
        return $"Department info: {id}";
    }
    [HttpPost]
    public object Create(Department department)
    {
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
}

using Microsoft.AspNetCore.Mvc;

namespace Departments.Controllers;

public class HomeController() : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}

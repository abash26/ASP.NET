using Departments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Departments.Views.Shared.Components.List
{
    [ViewComponent]
    public class ListViewComponent(IDepartmentsRepository departmentsRepository) : ViewComponent
    {
        private readonly IDepartmentsRepository _departmentsRepository = departmentsRepository;

        public IViewComponentResult Invoke(string? filter)
        {
            var departments = _departmentsRepository.GetDepartments(filter);
            return View(departments);
        }
    }
}

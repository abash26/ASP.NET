
namespace Departments.Models;

public interface IDepartmentsRepository
{
    void AddDepartment(Department? Department);
    bool DeleteDepartment(Department? Department);
    Department? GetDepartmentById(int id);
    List<Department> GetDepartments(string? filter);

    bool UpdateDepartment(Department? Department);
}
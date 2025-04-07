
namespace Routing.Models;

public interface IEmployeesRepository
{
    void AddEmployee(Employee? employee);
    bool DeleteEmployee(Employee? employee);
    List<Employee> GetEmployees();
    bool UpdateEmployee(Employee? employee);
    Employee? GetEmployeeById(int id);
}
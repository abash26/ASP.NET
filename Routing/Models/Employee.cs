using System.ComponentModel.DataAnnotations;

namespace Routing.Models;

public class Employee(int id, string name, string position, double salary)
{
    public int Id { get; set; } = id;

    [Required]
    public string Name { get; set; } = name;

    public string Position { get; set; } = position;

    [Required]
    [Range(50000, 200000)]
    public double Salary { get; set; } = salary;
}

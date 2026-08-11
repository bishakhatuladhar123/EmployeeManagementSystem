using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<bool> AddEmployeeAsync(Employee employee);
    }
}
























//Task<List<Employee>> GetAllEmployeeAsync();
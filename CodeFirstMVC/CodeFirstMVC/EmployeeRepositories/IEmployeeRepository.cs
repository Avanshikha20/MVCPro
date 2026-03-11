using CodeFirstMVC.Models;

namespace CodeFirstMVC.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task Add(Employee employee);
        Task Update(int id,Employee employee);
        Task Delete(int id);
    }
}

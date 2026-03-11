using CodeFirstMVC.Context;
using CodeFirstMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstMVC.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task Add(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            try
            {
                _employeeContext.SaveChanges();
            }
            catch
            {
                throw;
            }
           //throw new NotImplementedException();
        }

        //public Task Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var emp = await _employeeContext.Employees.ToListAsync();
                return emp;
            }
            catch
            {
                throw;
            }

        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employeeContext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return null;
                }
                return employee;

            }
            catch
            {
                throw;
            }
        }

        public async Task Update(int id, Employee employee)
        {
            try
            {
                Employee emp = await _employeeContext.Employees.FindAsync(id);
                if (emp != null)
                {
                    emp.EmpName = employee.EmpName;
                    emp.Address = employee.Address;
                    emp.Salary = employee.Salary;
                    emp.EmailAddress = employee.EmailAddress;
                    _employeeContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task Delete(int id)
        {
            try
            {
                Employee employee = await _employeeContext.Employees.FindAsync(id);
                _employeeContext.Employees.Remove(employee);
                _employeeContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}

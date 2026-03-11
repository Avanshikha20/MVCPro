using AspNetCoreGeneratedDocument;
using CodeFirstMVC.Context;
using CodeFirstMVC.EmployeeRepositories;
using CodeFirstMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CodeFirstMVC.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //private EmployeeContext employeeContext;
        //public EmployeeController(EmployeeContext _employeeContext)
        //{
        //    employeeContext = _employeeContext;
        //}
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //return _employeeRepository.GetAllEmployees() != null ?
            //    View(await _employeeRepository.GetAllEmployees()) :
            //    Problem("Entity set 'EmployeeContext.Employees' is not null");

            return View(await _employeeRepository.GetAllEmployees());
        }

      
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpName,Address,Salary,EmailAddress")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpName,Address,Salary,EmailAddress")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeRepository.Update(id, employee);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return Problem("entity set 'EmployeeContext.Employees'is null");
            }

            await _employeeRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }






    }
}

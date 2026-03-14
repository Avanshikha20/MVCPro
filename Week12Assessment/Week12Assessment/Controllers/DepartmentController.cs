using Microsoft.AspNetCore.Mvc;
using Week12Assessment.Data;
using Week12Assessment.Models;
using System.Linq;

namespace Week12Assessment.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Departments.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var dept = _context.Departments.Find(id);
            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var dept = _context.Departments.Find(id);
            return View(dept);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int DepartmentId)
        {
            var students = _context.Students
                .Where(s => s.DepartmentId == DepartmentId)
                .ToList();

            var courses = _context.Courses
                .Where(c => c.DepartmentId == DepartmentId)
                .ToList();

            if (students.Any() || courses.Any())
            {
                TempData["Error"] =
                "Cannot delete department because students or courses are assigned.";

                return RedirectToAction("Index");
            }

            var dept = _context.Departments.Find(DepartmentId);

            if (dept != null)
            {
                _context.Departments.Remove(dept);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
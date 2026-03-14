using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Week12Assessment.Data;
using Week12Assessment.Models;
using System.Linq;

namespace Week12Assessment.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                _context.Departments,
                "DepartmentId",
                "DepartmentName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Find(id);

            ViewBag.Departments = new SelectList(
                _context.Departments,
                "DepartmentId",
                "DepartmentName");

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Find(id);
            return View(course);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int CourseId)
        {
            var students = _context.Students
                .Where(s => s.CourseId == CourseId)
                .ToList();

            if (students.Any())
            {
                TempData["Error"] = "Cannot delete course because students are assigned.";
                return RedirectToAction("Index");
            }

            var course = _context.Courses.Find(CourseId);

            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
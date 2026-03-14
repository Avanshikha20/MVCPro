using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Week12Assessment.Data;
using Week12Assessment.Models;
using System.Linq;

namespace Week12Assessment.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Students.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                _context.Departments,
                "DepartmentId",
                "DepartmentName");

            ViewBag.Courses = new SelectList(
                _context.Courses,
                "CourseId",
                "CourseName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            ViewBag.Departments = new SelectList(
                _context.Departments,
                "DepartmentId",
                "DepartmentName");

            ViewBag.Courses = new SelectList(
                _context.Courses,
                "CourseId",
                "CourseName");

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(student);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int StudentId)
        {
            var student = _context.Students.Find(StudentId);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
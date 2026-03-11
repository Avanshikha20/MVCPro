using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        // ==============================
        // LIST ALL STUDENTS
        // ==============================
        public IActionResult Index()
        {
            var students = _context.Students
                                   .Include(s => s.Department)
                                   .ToList();

            return View(students);
        }

        // ==============================
        // CREATE STUDENT (GET)
        // ==============================
        public IActionResult Create()
        {
            LoadDepartments();
            return View();
        }

        // ==============================
        // CREATE STUDENT (POST)
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // reload dropdown if validation fails
            LoadDepartments();
            return View(student);
        }

        // ==============================
        // STUDENT DETAILS
        // ==============================
        public IActionResult Details(int id)
        {
            var student = _context.Students
                                  .Include(s => s.Department)
                                  .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // ==============================
        // DELETE (GET)
        // ==============================
        public IActionResult Delete(int id)
        {
            var student = _context.Students
                                  .Include(s => s.Department)
                                  .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // ==============================
        // DELETE (POST)
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int StudentId)
        {
            var student = _context.Students.Find(StudentId);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Search(string name, int? departmentId)
        {
            var students = _context.Students
                .Include(s => s.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                students = students.Where(s => s.Name.Contains(name));
            }

            if (departmentId.HasValue && departmentId > 0)
            {
                students = students.Where(s => s.DepartmentId == departmentId);
            }

            return View("Index", students.ToList());
        }

        // ==============================
        // JSON DATA
        // ==============================
        public JsonResult JsonData()
        {
            var students = _context.Students.ToList();
            return Json(students);
        }

        // ==============================
        // MESSAGE
        // ==============================
        public ContentResult Message()
        {
            return Content("Student registered successfully");
        }

        // ==============================
        // REDIRECT
        // ==============================
        public IActionResult RedirectToDepartment()
        {
            return Redirect("/Department/Index");
        }

        // ==============================
        // HELPER METHOD FOR DROPDOWN
        // ==============================
        private void LoadDepartments()
        {
            ViewBag.Departments = new SelectList(_context.Departments,
                                                 "DepartmentId",
                                                 "DepartmentName");
        }

        // ==============================
        // EDIT (GET)
        // ==============================
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        // ==============================
        // EDIT (POST)
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Week12Assessment.Data;
using Week12Assessment.ViewModels;
using System.Linq;

namespace Week12Assessment.Controllers
{
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            var student = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefault(s => s.Email == email);

            if (student == null)
                return RedirectToAction("Login", "Account");

            StudentProfileViewModel vm = new StudentProfileViewModel
            {
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department.DepartmentName,
                CourseName = student.Course.CourseName,
                Duration = student.Course.Duration,
                Fees = student.Course.Fees
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult UpdateProfile(StudentProfileViewModel model)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Email == model.Email);

            if (student != null)
            {
                student.PhoneNumber = model.PhoneNumber;
                student.Address = model.Address;

                _context.SaveChanges();
            }

            return RedirectToAction("Profile");
        }
    }
}
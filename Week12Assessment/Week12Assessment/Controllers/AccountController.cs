using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Week12Assessment.Data;
using Week12Assessment.Models;
using Week12Assessment.ViewModels;

namespace Week12Assessment.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email
                && u.Password == model.Password);

            if (user != null)
            {
                Random rnd = new Random();
                int otp = rnd.Next(100000, 999999);

                HttpContext.Session.SetString("OTP", otp.ToString());
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role);

                TempData["OTP"] = otp.ToString();   // show OTP

                return RedirectToAction("VerifyOtp");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult VerifyOtp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult VerifyOtp(VerifyOtpViewModel model)
        {
            string sessionOtp = HttpContext.Session.GetString("OTP");

            if (model.OTP == sessionOtp)
            {
                string role = HttpContext.Session.GetString("UserRole");

                if (role == "Teacher")
                    return RedirectToAction("Index", "TeacherDashboard");

                if (role == "Student")
                    return RedirectToAction("Index", "StudentDashboard");
            }

            ViewBag.Error = "Invalid OTP";
            return View();
        }
    }
}
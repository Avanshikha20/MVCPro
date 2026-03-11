using Microsoft.AspNetCore.Mvc;

namespace CodeFirstMVC.Controllers
{
    public class ActionResultDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // Redirect
        public IActionResult Google()
        {
            return Redirect("https://www.google.com");
        }

        // Permanent Redirect
        public IActionResult Microsoft()
        {
            return RedirectPermanent("https://www.microsoft.com");
        }
        public IActionResult Login()
        {
            return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            return Content("Welcome to Dashboard");
        }

        // RedirectToRoute
        public IActionResult RouteDemo()
        {
            return RedirectToRoute(new
            {
                controller = "Home",
                action = "Index"
            });
        }
        // JSON Result
        public IActionResult StudentJson()
        {
            var student = new
            {
                Id = 1,
                Name = "Ravi",
                Course = ".NET Core"
            };

            return Json(student);
        }
        // OK Result
        public IActionResult Success()
        {
            return Ok("Operation successful");
        }
        // Bad Request
        public IActionResult CheckAge(int age)
        {
            if (age < 18)
            {
                return BadRequest("Age must be above 18");
            }

            return Ok("Valid age");
        }
    }
}

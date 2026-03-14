using Microsoft.AspNetCore.Mvc;

namespace Week12Assessment.Controllers
{
    public class TeacherDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
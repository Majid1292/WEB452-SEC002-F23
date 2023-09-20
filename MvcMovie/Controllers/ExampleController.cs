using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name = "John", int num = 1)
        {
            ViewData["Message"] = "Hi " + name;
            ViewData["Number"] = num;
            return View();
        }
    }
}

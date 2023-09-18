using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class ExampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Welcome(string name = "John ", int ID = 1)
        {
            return "Your name is: " + name + " and Your ID is :" + ID;
        }
    }
}

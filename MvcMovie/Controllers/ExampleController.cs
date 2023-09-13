using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class ExampleController : Controller
    {
        public string Index()
        {
            return "This a test for Index method";
        }

        public string Welcome(string name = "John", int ID = 1)
        {
            return "Your name is: " + name + " and Your ID is :" + ID;
        }
    }
}

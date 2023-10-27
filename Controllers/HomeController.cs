using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

 
    }
}

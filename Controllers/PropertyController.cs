using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult General() // this view will return both leilighet and hus
        {
            return View();
        }

        public IActionResult Apartment() // only apartments
        {
            return View();
        }

        public IActionResult House() // only houses
        {
            return View();
        }
    }
}

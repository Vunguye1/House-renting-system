using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult GeneralGrid() // this view will return both leilighet and hus
        {
            return View();
        }

        public IActionResult GeneralTable() // general table view
        {
            return View();
        }

        public IActionResult ApartmentGrid() // only apartments, grid layout
        {
            return View();
        }

        public IActionResult ApartmentTable() // only apartments, table layout
        {
            return View();
        }

        public IActionResult HouseGrid() // only houses, grid layout
        {
            return View();
        }

        public IActionResult HouseTable() // only houses, table layout
        {
            return View();
        }
    }
}

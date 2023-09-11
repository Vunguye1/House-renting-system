using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class PropertyController : Controller
    {

        private readonly PropertyDbContext _propertyDbContext;

        public PropertyController(PropertyDbContext propertyDbContext)
        {
            _propertyDbContext = propertyDbContext;
        }

        public IActionResult GeneralGrid() // this view will return both leilighet and hus
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "GeneralGrid");
            return View(listmodel);
        }

        public IActionResult GeneralTable() // general table view
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        public IActionResult ApartmentGrid() // only apartments, grid layout
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "ApartmentGrid");
            return View(listmodel);
        }

        public IActionResult ApartmentTable() // only apartments, table layout
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "ApartmentTable");
            return View(listmodel);
        }

        public IActionResult HouseGrid() // only houses, grid layout
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "HouseGrid");
            return View(listmodel);
        }

        public IActionResult HouseTable() // only houses, table layout
        {
            List<Property> propertylist = _propertyDbContext.Properties.ToList();
            var listmodel = new PropertyListViewModel(propertylist, "HouseTable");
            return View(listmodel);
        }
    }
}

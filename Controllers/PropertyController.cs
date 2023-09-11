using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> GeneralGrid() // this view will return both leilighet and hus
        {
            List<Property> propertylist = await _propertyDbContext.Properties.ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "GeneralGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> GeneralTable() // general table view
        {
            List<Property> propertylist = await _propertyDbContext.Properties.ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentGrid() // only apartments, grid layout
        {
            List<Property> propertylist = await _propertyDbContext.Properties.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "ApartmentGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentTable() // only apartments, table layout
        {
            List<Property> propertylist = await _propertyDbContext.Properties.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "ApartmentTable");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseGrid() // only houses, grid layout
        {
            List<Property> propertylist = await _propertyDbContext.Properties.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "HouseGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseTable() // only houses, table layout
        {
            List<Property> propertylist = await _propertyDbContext.Properties.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new PropertyListViewModel(propertylist, "HouseTable");
            return View(listmodel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _propertyDbContext.Properties.FirstOrDefaultAsync(i => i.PropertyId == id);
            if (item == null)
                return NotFound();
            return View(item);
        }
    }
}

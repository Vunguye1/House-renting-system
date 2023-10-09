using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class RealestateController : Controller
    {

        private readonly RealestateDbContext _realestateDbContext;

        public RealestateController(RealestateDbContext realestateDbContext)
        {
            _realestateDbContext = realestateDbContext;
        }

        public async Task<IActionResult> GeneralGrid() // this view will return both leilighet and hus
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> GeneralTable() // general table view
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentGrid() // only apartments, grid layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "ApartmentGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentTable() // only apartments, table layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "ApartmentTable");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseGrid() // only houses, grid layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "HouseGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseTable() // only houses, table layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "HouseTable");
            return View(listmodel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _realestateDbContext.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Realestate property)
        {
            if (ModelState.IsValid)
            {
                _realestateDbContext.Realestates.Add(property);
                await _realestateDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(GeneralGrid));
            }
            return View(property);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _realestateDbContext.Realestates.FindAsync(id);

            if (item == null)
            {
                return BadRequest("item not found");
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Realestate realestate)
        {

            if (ModelState.IsValid)
            {
                _realestateDbContext.Realestates.Update(realestate);
                await _realestateDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GeneralGrid));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var realestate = await _realestateDbContext.Realestates.FindAsync(id);
            if (realestate == null)
            {
                return NotFound();
            }
            return View(realestate);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realestate = _realestateDbContext.Realestates.Find(id);
            if (realestate == null)
            {
                return NotFound();
            }
            _realestateDbContext.Realestates.Remove(realestate);
            await _realestateDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(GeneralGrid));
        }


    }
}

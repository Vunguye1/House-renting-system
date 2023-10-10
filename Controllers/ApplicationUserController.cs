using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;

        public ApplicationUserController(RealestateDbContext realestateDbContext)
        {
            _realestateDbContext = realestateDbContext;
        }
        

        public async Task<IActionResult> ListRealestateByOwner(string ownerId) // List all properties the user register on the system
        {
            List<Realestate> realestates = await _realestateDbContext.Realestates.Where(p => p.UserId == ownerId).ToListAsync();
            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate");

            return View(listmodel);
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
            return RedirectToAction(nameof(ListRealestateByOwner));
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

            return RedirectToAction(nameof(ListRealestateByOwner));
        }
    }
}

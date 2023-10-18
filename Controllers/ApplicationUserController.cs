using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;
using Project1.DAL;

namespace Project1.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;
        private readonly IAdminRepository _adminRepository;

        public ApplicationUserController(RealestateDbContext realestateDbContext, IAdminRepository adminRepository)
        {
            _realestateDbContext = realestateDbContext;
            _adminRepository = adminRepository;
        }

        // this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
        public IQueryable<Realestate> GetActiveRealestates()
        {
            return _realestateDbContext.Realestates.Where(r => !r.IsDeleted);
        }

        public async Task<IActionResult> ListRealestateByOwner(string ownerId) // List all properties the user register on the system
        {
            List<Realestate> realestates = await GetActiveRealestates()
                .Where(p => p.UserId == ownerId).ToListAsync();

            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate");

            return View(listmodel);
        }

        public async Task<IActionResult> ListRentHistory(string userId) // List all properties the user register on the system
        {
            List<Rent> renthistory = await _realestateDbContext.Rent.Where(p => p.UserId == userId).ToListAsync();

            return View(renthistory);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRealEstate(int id)
        {
            var item = await _realestateDbContext.Realestates.FindAsync(id);

            if (item == null)
            {
                return BadRequest("item not found");
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
        {

            if (ModelState.IsValid)
            {
                _realestateDbContext.Realestates.Update(realestate);
                await _realestateDbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ListRealestateByOwner));
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

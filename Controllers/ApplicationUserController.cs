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
        private readonly IApplicationUserRepository _applicationUserRepository;

        public ApplicationUserController(RealestateDbContext realestateDbContext, IApplicationUserRepository applicationUserRepository)
        {
            _realestateDbContext = realestateDbContext;
            _applicationUserRepository = applicationUserRepository;
        }

      

        public async Task<IActionResult> ListRealestateByOwner(string ownerId) // List all properties the user register on the system
        {
            //List<Realestate> realestates = await GetActiveRealestates()
            //    .Where(p => p.UserId == ownerId).ToListAsync();

            //er tolistAsync feil å bruke her, bør vi lage egen repo metode til denne under?
            List<Realestate> realestates = await _applicationUserRepository.GetActiveRealestates().Where(p => p.UserId == ownerId).ToListAsync();

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
            var item = await _applicationUserRepository.GetRealestateById(id);

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
                
                await _applicationUserRepository.Update(realestate);
                
                
            }
            return RedirectToAction(nameof(ListRealestateByOwner));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            await _applicationUserRepository.Delete(id);
            return RedirectToAction(nameof(ListRealestateByOwner));
        }
    }
}

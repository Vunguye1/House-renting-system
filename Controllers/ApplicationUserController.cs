using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DAL;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;
        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager

        public ApplicationUserController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IRealestateRepository realestateRepository)
        {
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
        public IQueryable<Realestate> GetActiveRealestates()
        {
            return _realestateDbContext.Realestates.Where(r => !r.IsDeleted);
        }

        public async Task<IActionResult> ListRealestateByOwner() // List all properties the user register on the system
        {
            var curruser = await _userManager.GetUserAsync(User);

            if (curruser == null)
            {
                return NotFound("User not found");
            }
            List<Realestate> realestates = await GetActiveRealestates()
                .Where(p => p.UserId == curruser.Id).ToListAsync();

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
            var realestate = await _realestateDbContext.Realestates.FindAsync(id);

            if (realestate == null)
            {
                return BadRequest("item not found");
            }
            return View(realestate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
        {
            var curruser = await _userManager.GetUserAsync(User);

            realestate.UserId = curruser.Id;

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

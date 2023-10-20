using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DAL;
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
        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager

        public ApplicationUserController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IRealestateRepository realestateRepository)
        {
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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

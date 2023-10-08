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
        
        public async Task<IActionResult> ListAllUsers() // List all users registered in database. Testing purpose
        {
            List<ApplicationUser> users = await _realestateDbContext.User.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> ListRealestateByOwner(string ownerId) // List all properties the user register on the system
        {
            List<Realestate> realestates = await _realestateDbContext.Realestates.Where(p => p.UserId == ownerId).ToListAsync();
            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate");

            return View(listmodel);
        }
    }
}

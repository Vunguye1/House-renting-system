using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    public class ApplicationUserController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;

        public ApplicationUserController(RealestateDbContext realestateDbContext)
        {
            _realestateDbContext = realestateDbContext;
        }
        
        public async Task<IActionResult> ListAllUsers()
        {
            List<ApplicationUser> users = await _realestateDbContext.User.ToListAsync();
            return View(users);
        }
    }
}

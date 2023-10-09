using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.Controllers
{
    [Authorize(Roles = "Admin")] // this controller contains functions that are only for admin
    public class AdminController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;

        public AdminController(RealestateDbContext realestateDbContext)
        {
            _realestateDbContext = realestateDbContext;
        }

        public async Task<IActionResult> ListAllUsers() // List all users registered in database. Testing purpose
        {
            List<ApplicationUser> users = await _realestateDbContext.User.ToListAsync();
            return View(users);
        }
    }
}

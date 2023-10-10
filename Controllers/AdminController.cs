using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;

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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListAllUsers() // List all users registered in database. Testing purpose
        {
            List<ApplicationUser> users = await _realestateDbContext.User.ToListAsync();
            return View(users);
        }



        public async Task<IActionResult> ListAllRealestates() // List all existing real estates
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
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
            return RedirectToAction(nameof(ListAllRealestates));
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

            return RedirectToAction(nameof(ListAllRealestates));
        }
    }
}

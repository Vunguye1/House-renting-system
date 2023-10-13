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

        // ------------------------ Real estates management by admin ------------------------
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
            return RedirectToAction(nameof(ListAllRealestates));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRealEstate(int id)
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

        // ------------------------ Done real estates management by admin ------------------------


        // ------------------------ Users management ------------------------

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userid)
        {
            var user = await _realestateDbContext.User.FindAsync(userid);

            if (user == null)
            {
                return BadRequest("User not found");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _realestateDbContext.User.FirstOrDefault(u => u.Id == user.Id);

                if (existingUser != null)
                {
                    // Update user properties
                    existingUser.FirstName = user.FirstName; // Replace with actual property names
                    existingUser.LastName = user.LastName; // Replace with actual property names
                    existingUser.PhoneNumber = user.PhoneNumber; // Replace with actual property names



                    _realestateDbContext.Entry(existingUser).State = EntityState.Modified;

                    try
                    {
                        await _realestateDbContext.SaveChangesAsync();
                        return RedirectToAction(nameof(ListAllUsers));
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        // Handle the concurrency exception, if needed.
                        BadRequest(ex);
                    }
                }
                else
                {
                    // Handle the case where the user is not found in the database.
                    NotFound("User not found");
                }
            }

            // If ModelState is not valid, return to the edit view to display validation errors.
            return RedirectToAction(nameof(ListAllUsers));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteUserConfirmed(string userid)
        {
            var user = _realestateDbContext.User.Find(userid);
            if (user == null)
            {
                return NotFound();
            }
            _realestateDbContext.User.Remove(user);
            await _realestateDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(ListAllUsers));
        }


    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;
using Project1.DAL;

namespace Project1.Controllers
{
    [Authorize(Roles = "Admin")] // this controller contains functions that are only for admin
    public class AdminController : Controller
    {
        private readonly RealestateDbContext _realestateDbContext;
        private readonly IAdminRepository _adminRepository;

        public AdminController(RealestateDbContext realestateDbContext, IAdminRepository adminRepository)
        {
            _realestateDbContext = realestateDbContext;
            _adminRepository = adminRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //// this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
        //public IQueryable<Realestate> GetActiveRealestates()
        //{
        //    return _realestateDbContext.Realestates.Where(r => !r.IsDeleted);
        //}

        public async Task<IActionResult> ListAllUsers() // List all users registered in database. Testing purpose
        {
            var usersList = await _adminRepository.ListAllUsers();
            return View(usersList);
        }


        


    public async Task<IActionResult> ListAllRealestates() // List all existing real estates
        {
            //List<Realestate> propertylist = await GetActiveRealestates().ToListAsync();
            var propertylist = await _adminRepository.ListAllRealestates();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        // ------------------------ Real estates management by admin ------------------------
        [HttpGet]
        public async Task<IActionResult> UpdateRealEstate(int id)
        {
            //var item = await _realestateDbContext.Realestates.FindAsync(id);
            var item = await _adminRepository.GetRealestateById(id);

            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
        {

            if (ModelState.IsValid)
            {
                await _adminRepository.UpdateRealestate(realestate);
            }
            return RedirectToAction(nameof(ListAllRealestates));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _adminRepository.DeleteRealestate(id);
            return RedirectToAction(nameof(ListAllRealestates));
        }

        // ------------------------ Done real estates management by admin ------------------------


        // ------------------------ Users management ------------------------

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userid)
        {
            //var user = await _realestateDbContext.User.FindAsync(userid);
            var user = await _adminRepository.GetUserById(userid);

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
            await _adminRepository.DeleteUser(userid);
            return RedirectToAction(nameof(ListAllUsers));
        }


    }
}

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
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager

      

        public ApplicationUserController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IApplicationUserRepository applicationUserRepository)
        {
            
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationUserRepository = applicationUserRepository;
        }

      

        public async Task<IActionResult> ListRealestateByOwner() // List all properties the user register on the system
        {
            var curruser = await _userManager.GetUserAsync(User);

            if (curruser == null)
            {
                return NotFound("User not found");
            }


            var realestates = await _applicationUserRepository.GetRealestateByOwner(curruser);


            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate");

            return View(listmodel);
        }

        //MANGLER
        public async Task<IActionResult> ListRentHistory(string userId) // List all properties the user register on the system
        {
            var renthistory = await _applicationUserRepository.ListRentHistory(userId);

            return View(renthistory);
        }

        //ENDre
        [HttpGet]
        public async Task<IActionResult> UpdateRealEstate(int id)
        {
            var realestate = await _applicationUserRepository.GetRealestateById(id);

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
                
                bool returnok = await _applicationUserRepository.Update(realestate);

                if (!returnok)
                {
                    return BadRequest("Update failed"); 
                }
                
                
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

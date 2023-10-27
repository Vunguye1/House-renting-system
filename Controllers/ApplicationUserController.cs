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
        private readonly ILogger<ApplicationUserController> _logger;

      

        public ApplicationUserController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IApplicationUserRepository applicationUserRepository, ILogger<ApplicationUserController> logger)
        {
            
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationUserRepository = applicationUserRepository;
            _logger = logger;
        }

      

        public async Task<IActionResult> ListRealestateByOwner() // List all properties the user register on the system
        {
            var curruser = await _userManager.GetUserAsync(User);

            if (curruser == null)
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }


            var realestates = await _applicationUserRepository.GetRealestateByOwner(curruser);

            if (realestates == null)
            {
                _logger.LogError("[ApplicationUserController] Realestate by owner not found while executing ");
                return NotFound("Realestate by owner not found _applicationUserRepository.GetRealestateByOwner(curruser)");
            }

            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate");
            return View(listmodel);
        }

        
        public async Task<IActionResult> ListRentHistory() // List all properties the user register on the system
        {

            var curruser = await _userManager.GetUserAsync(User);

            if (curruser == null)
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }

            var renthistory = await _applicationUserRepository.ListRentHistory(curruser);

            if (renthistory == null)
            {
                _logger.LogError("[ApplicationUserController] Rent history list not found while excecuting _applicationUserRepository.ListRentHistory()");
                return NotFound("Rent history not found");
            }

            return View(renthistory);
        }

        
        [HttpGet]
        public async Task<IActionResult> UpdateRealEstate(int id)
        {
            var realestate = await _applicationUserRepository.GetRealestateById(id);

            if (realestate == null)
            {
                _logger.LogError("[ApplicationUserController] Realestate not found when updating the realestateId {RealestateId:0000}", id);
                return BadRequest("realestate not found for realestateId ");
            }
            return View(realestate);
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
        {
            var curruser = await _userManager.GetUserAsync(User);

            if (curruser == null)
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }

            realestate.UserId = curruser.Id;

            if (ModelState.IsValid)
            {
                
                bool returnOk = await _applicationUserRepository.Update(realestate);

                if (returnOk)
                {
                    return RedirectToAction(nameof(ListRealestateByOwner));
                }  
            }
            _logger.LogWarning("[ApplicationUserController] Realestate update failed {@realestate}", realestate);
            return View(realestate);
            
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            bool returnOk= await _applicationUserRepository.Delete(id);
            if (!returnOk)
            {
                _logger.LogError("[ApplicationUserController] Realestate deletion failed for the RealestateId {RealestateId:0000}", id);
                return BadRequest("Realestate deletion failed");
            }
            return RedirectToAction(nameof(ListRealestateByOwner));
        }
    }
}

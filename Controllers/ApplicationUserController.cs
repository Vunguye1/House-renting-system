using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.DAL;
using Project1.Models;
using Project1.ViewModels;


namespace Project1.Controllers
{
    [Authorize(Roles = "Default")] // this controller contains functions that are only for default user

    public class ApplicationUserController : Controller
    {
       
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager
        private readonly ILogger<ApplicationUserController> _logger;

      

        public ApplicationUserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IApplicationUserRepository applicationUserRepository, ILogger<ApplicationUserController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationUserRepository = applicationUserRepository;
            _logger = logger;
        }

      

        public async Task<IActionResult> ListRealestateByOwner() // List all properties the user register on the system So that the user can get an overview of all the properties it has out for rent
        {
            var curruser = await _userManager.GetUserAsync(User); //first we get the user

            if (curruser == null) // if the user not found we log
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }

            var realestates = await _applicationUserRepository.GetRealestateByOwner(curruser); //else we try to get the realestates by owner

            if (realestates == null) //if the list is null we log
            {
                _logger.LogError("[ApplicationUserController] Realestate by owner not found while executing ");
                return NotFound("Realestate by owner not found _applicationUserRepository.GetRealestateByOwner(curruser)");
            }

            var listmodel = new RealestateListViewModel(realestates, "Your registered real estate"); 
            return View(listmodel); //Lists all of the users realestates in view
        }

        
        public async Task<IActionResult> ListRentHistory() // List rent history for user
        {

            var curruser = await _userManager.GetUserAsync(User); //try to get current user

            if (curruser == null) //if user is null we log
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }

            var renthistory = await _applicationUserRepository.ListRentHistory(curruser); //else we try to get list of all erlier rents

            if (renthistory == null) //if rent lists is null we log
            {
                _logger.LogError("[ApplicationUserController] Rent history list not found while excecuting _applicationUserRepository.ListRentHistory()");
                return NotFound("Rent history not found");
            }

            return View(renthistory); // else we want to show the history in view
        }

        
        [HttpGet] //GET-display form for updating the real estate
        public async Task<IActionResult> UpdateRealEstate(int id)
        {
            var curruser = await _userManager.GetUserAsync(User); //try to get current user

            if (curruser.Realestate != null) { // check if this user has any real estate, if yes

                foreach (var rs in curruser.Realestate)
                {
                    if (rs.RealestateId == id) // we find the related real estate user wants to update
                    {
                        var realestate = await _applicationUserRepository.GetRealestateById(id); // get this real esate

                        if (realestate == null) // if this real estate id is not existed, we log error
                        {
                            _logger.LogError("[ApplicationUserController] Realestate not found when updating th realestateId {RealestateId:0000}", id);
                            return BadRequest("realestate not found for realestateId ");
                        }
                        return View(realestate);
                    }
                }
            }
            return NotFound("404! This real estate is not yours"); // if some one try to get access to others' real estate by copying URL, return a 404 message
            
            
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate) 
        {
            var curruser = await _userManager.GetUserAsync(User); //try to get current user

            if (curruser == null) //if user is null we log
            {
                _logger.LogError("[ApplicationUserController] User not found while executing _userManager.GetUserAsync");
                return NotFound("User not found");
            }

            realestate.UserId = curruser.Id; // bind userid of real estate of our currently logged in user

            if (ModelState.IsValid)
            {
                
                bool returnOk = await _applicationUserRepository.Update(realestate); // update this real estate

                if (returnOk)
                {
                    return RedirectToAction(nameof(ListRealestateByOwner));
                }  
            }
            _logger.LogWarning("[ApplicationUserController] Realestate update failed {@realestate}", realestate);
            return View(realestate);
            
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id) //Users have the ability to remove their listed rental properties
        {
            
            bool returnOk= await _applicationUserRepository.Delete(id); //try to delete user
            if (!returnOk) //if not OK we log
            {
                _logger.LogError("[ApplicationUserController] Realestate deletion failed for the RealestateId {RealestateId:0000}", id);
                return BadRequest("Realestate deletion failed");
            }
            return RedirectToAction(nameof(ListRealestateByOwner));
        }
    }
}

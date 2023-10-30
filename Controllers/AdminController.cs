using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Project1.Models;
using Project1.ViewModels;
using Project1.DAL;


namespace Project1.Controllers;

[Authorize(Roles = "Admin")] //Only Admin can do these requests
public class AdminController : Controller
{
    private readonly RealestateDbContext _realestateDbContext;
    private readonly IAdminRepository _adminRepository;
    private readonly ILogger<AdminController> _logger; //adding private readonly logger

    public AdminController(RealestateDbContext realestateDbContext, IAdminRepository adminRepository, ILogger<AdminController> logger)
    {
        _realestateDbContext = realestateDbContext;
        _adminRepository = adminRepository;
        _logger = logger;
    }

    public IActionResult Index() //Responsible for displaying the admin “main page”
    {
        return View();
    }

    public async Task<IActionResult> ListAllUsers() // Lists all users that are registered in the database
    {
        var usersList = await _adminRepository.ListAllUsers(); //get all users with repo method
        if (usersList == null) //check if the list is null
        {
            _logger.LogError("[AdminController] User list not found while executing _adminRepository.ListAllUsers()");
            return NotFound("User list not found");
        }
        return View(usersList);//return the list of user in view if list not empty
    }





    public async Task<IActionResult> ListAllRealestates() // List all existing real estates 
    {

        var propertylist = await _adminRepository.ListAllRealestates(); //get all realestates with repo method
        if (propertylist == null) //chekc if list is null
        {
            _logger.LogError("[AdminController] Property list not found while executing _adminRepository.ListAllRealestates()");
            return NotFound("Property list not found");
        }
        var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
        return View(listmodel); //list realestates in table view

    }

    // ------------------------ Real estates management by admin ------------------------
    [HttpGet] // GET request- used to display the form for updating an existing real estate
    public async Task<IActionResult> UpdateRealEstate(int id) //  admin has the capability to update a particular real estate 
    {
        var item = await _adminRepository.GetRealestateById(id); //get the realestate we want to update.

        if (item == null)
        {
            _logger.LogError("[AdminController] Realestate not found when updating the RealestateId {RealestateId:0000}", id);
            return BadRequest("Realestate not found for the RealestateId");
        }

        return View(item);
    }


    [HttpPost] //POST request - Used to handle the submission of the edited form
    public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
    {

        if (ModelState.IsValid)
        {

            bool returnOk = await _adminRepository.UpdateRealestate(realestate); //tries to update realestate
            if (returnOk) //if OK we return to the view of all realestates.
            {
                return RedirectToAction(nameof(ListAllRealestates));
            }
        }
        _logger.LogWarning("[AdminController] Realestate update failed {@realestate]", realestate);

        return View(realestate);

    }

    // ------------------------ Done real estates management by admin ------------------------


    // ------------------------ Users management ------------------------

    [HttpGet] // GET request- used to display the form for updating a user.
    public async Task<IActionResult> UpdateUser(string userid) // admin has the capability to update a particular user
    {
        var user = await _adminRepository.GetUserById(userid); //Gets the user we want to update

        if (user == null) // if user not found we log
        {
            _logger.LogError("[AdminController] User not found when updating the UserId {UserId:0000}", userid);
            return BadRequest("User not found");
        }
        return View(user); // else we return the user we want to update in form view
    }


    [HttpPost] //POST request - Used to handle the submission of the edited user.
    public async Task<IActionResult> UpdateUser(ApplicationUser user) //
    {
        if (ModelState.IsValid)
        {

            var existingUser = await _adminRepository.GetUserById(user.Id);

            if (existingUser != null)
            {
                // Update user properties
                existingUser.FirstName = user.FirstName; // Replace with actual property names
                existingUser.LastName = user.LastName; // Replace with actual property names
                existingUser.PhoneNumber = user.PhoneNumber; // Replace with actual property names



                _realestateDbContext.Entry(existingUser).State = EntityState.Modified;



                bool returnOk = await _adminRepository.UpdateUser(user); //try to update user
                if (returnOk) //if OK we return to the list of all users
                {
                    return RedirectToAction(nameof(ListAllUsers));
                }
                _logger.LogWarning("[AdminController] User update failed {@item}", user);



            }
            else
            {
                _logger.LogError("[AdminController] User not found when updating the userId {UserId:0000}", user.Id);
                // Handle the case where the user is not found in the database.
                return BadRequest("User not found");
            }
        }

        // If ModelState is not valid, return to the edit view to display validation errors.
        return RedirectToAction(nameof(ListAllUsers));
    }



    [HttpPost]
    public async Task<IActionResult> DeleteUserConfirmed(string userid) //delete user 
    {
        bool returnok = await _adminRepository.DeleteUser(userid); //try do delte

        if (!returnok) //if not ok we log en return bad request
        {
            _logger.LogError("[AdminController] User deletion failed for the userId {Userid:0000}", userid);
            return BadRequest("User deletion failed");
        }

        return RedirectToAction(nameof(ListAllUsers));
    }



}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Project1.Models;
using Project1.ViewModels;
using Project1.DAL;


namespace Project1.Controllers;

    [Authorize(Roles = "Admin")] 
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
            if (usersList == null)
        {
            _logger.LogError("[AdminController] User list not found while executing _adminRepository.ListAllUsers()");
            return NotFound("User list not found");
        }
            return View(usersList);
        }


        


    public async Task<IActionResult> ListAllRealestates() // List all existing real estates
        {
            //List<Realestate> propertylist = await GetActiveRealestates().ToListAsync();
            var propertylist = await _adminRepository.ListAllRealestates();
            if (propertylist == null)
        {
            _logger.LogError("[AdminController] Property list not found while executing _adminRepository.ListAllRealestates()");
            return NotFound("Property list not found");
        }
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
                _logger.LogError("[AdminController] Realestate not found when updating the RealestateId {RealestateId:0000}", id);
                return BadRequest("Realestate not found for the RealestateId");
            }
            
            return View(item);
        }

    
        [HttpPost]
        public async Task<IActionResult> UpdateRealEstate(Realestate realestate)
        {

            if (ModelState.IsValid)
            {

                bool returnOk= await _adminRepository.UpdateRealestate(realestate);
            if (returnOk)
            {
                return RedirectToAction(nameof(ListAllRealestates));
            }
            }
            _logger.LogWarning("[AdminController] Realestate update failed {@realestate]", realestate);
        
            return View(realestate);
            
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool returnOk= await _adminRepository.DeleteRealestate(id);
            if (!returnOk)
            {
                _logger.LogError("[AdminController] Realestate deletion failed for the RealestateID {RealestateId:0000}", id);
                return BadRequest("Realestate deletion failed");
            }

        return RedirectToAction(nameof(ListAllRealestates));
        }

        // ------------------------ Done real estates management by admin ------------------------


        // ------------------------ Users management ------------------------

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userid)
        {
            var user = await _adminRepository.GetUserById(userid);

            if (user == null)
            {
                _logger.LogError("[AdminController] User not found when updating the UserId {UserId:0000}", userid);
                return BadRequest("User not found");
            }
            return View(user);
        }

    
        [HttpPost]
        public async Task<IActionResult> UpdateUser(ApplicationUser user)
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

                    
                       
                        bool returnOk= await _adminRepository.UpdateUser(user);
                        if (returnOk)
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
    public async Task<IActionResult> DeleteUserConfirmed(string userid)
    {
        bool returnok = await _adminRepository.DeleteUser(userid);

        if (!returnok)
        {
            _logger.LogError("[AdminController] User deletion failed for the userId {Userid:0000}", userid);
            return BadRequest("User deletion failed");
        }

        return RedirectToAction(nameof(ListAllUsers));
    }
        


    }


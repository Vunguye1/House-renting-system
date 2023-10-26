﻿using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.DAL;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class RealestateController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager
        private readonly RealestateDbContext _realestateDbContext;
        private readonly IRealestateRepository _realestateRepository;
        private readonly ILogger<RealestateController> _logger;



        public RealestateController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IRealestateRepository realestateRepository, ILogger<RealestateController> logger)
        {
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _realestateRepository = realestateRepository;
            _logger = logger;
        }

        public async Task<IActionResult> GeneralGrid() // this view will return both leilighet and hus
        {

            var propertylist = await _realestateRepository.GetAll();

            if (propertylist == null)
            {
                _logger.LogError("[RealestateController] Property list not found while executing _realestateRepository.GetAll()");
                return NotFound("Realestate list not found");
            }
            var listmodel = new RealestateListViewModel(propertylist, "GeneralGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> GeneralTable() // general table view
        {
            var propertylist = await _realestateRepository.GetAll();

            if (propertylist == null)
            {
                _logger.LogError("[RealestateController] Property list not found while executing _realestateRepository.GetAll()");
                return NotFound("Realestate list not found");

            }
            var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentGrid() // only apartments, grid layout
        {
            var apartmentonly = await _realestateRepository.GetOnlyApartment();

            if (apartmentonly == null)
            {
                _logger.LogError("[RealestateController] apartment list not found while executing _realestateRepository.GetOnlyApartment()");
                return NotFound("Apartment list not found");
            }

            var listmodel = new RealestateListViewModel(apartmentonly, "ApartmentGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentTable() // only apartments, table layout
        {
            var apartmentonly = await _realestateRepository.GetOnlyApartment();

            if (apartmentonly == null)
            {
                _logger.LogError("[RealestateController] apartment list not found while executing _realestateRepository.GetOnlyApartment()");
                return NotFound("Apartment list not found");
            }

            var listmodel = new RealestateListViewModel(apartmentonly, "ApartmentTable");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseGrid() // only houses, grid layout
        {
            var houseonly = await _realestateRepository.GetOnlyHouse();

            if (houseonly == null)
            {
                _logger.LogError("[RealestateController] House list not found while executing _realestateRepository.GetOnlyHouse()");
                return NotFound("House list not found");
            }
            var listmodel = new RealestateListViewModel(houseonly, "HouseGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseTable() // only houses, table layout
        {
            var houseonly = await _realestateRepository.GetOnlyHouse();

            if (houseonly == null)
            {
                _logger.LogError("[RealestateController] House list not found while executing _realestateRepository.GetOnlyHouse()");
                return NotFound("House list not found");
            }
            var listmodel = new RealestateListViewModel(houseonly, "HouseTable");
            return View(listmodel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _realestateRepository.GetRealestateById(id);
            if (item == null)
            {
                _logger.LogError("[RealestateController] Realestate found for the RealestateId {RealestateId:0000}", id);
                return NotFound("Realestate not found for the realestateId");
            }
            return View(item);
        }

        [Authorize(Roles = "Default, Admin")] // only admin or defaults user can register real estate
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Default, Admin")]  // only admin or defaults user can register real estate
        [HttpPost]
        public async Task<IActionResult> Create(Realestate property)
        {
            var user = await _userManager.GetUserAsync(User); // get currently logged in user
            property.UserId = user.Id; // bind newly registered house to the currently logged user
            property.imageurl = await FileUpload();

            if (user == null) // if no one is logged in
                
            {
                _logger.LogError("[RealestateController] user not found while executing  _userManager.GetUserAsync");
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            else
            {
                if (ModelState.IsValid)
                {
                    bool returnOK = await _realestateRepository.Create(property);
                    if (returnOK)
                    {
                        return RedirectToAction(nameof(GeneralGrid));

                    }
                }
                _logger.LogWarning("[RealestateController] Realestate creation failed {@property} ", property);
                return View(property);
            }
        }


        [Authorize(Roles = "Default")] // Authorized only to default user
        [HttpGet]
        public async Task<IActionResult> Rent(int realestateId)
        {

            // get real estate
            var realestate = await _realestateRepository.GetRealestateById(realestateId);
            if (realestate == null)
            {
                _logger.LogError("[RealestateController] realestate not found while executing GetRealestateById(realestateId)");
                return BadRequest("Realestate not found.");
            }

            // view model for renting function. With this model, we can keep track of which real estate user want to rent
            var viewModel = new RentViewModel
            {
                Realestate = realestate,
                Rent = new Rent() // display the targeted real estate to user
            };

            // use today as start day
            viewModel.Rent.RentDateFrom = DateTime.Today; 
            viewModel.Rent.RentDateTo = DateTime.Today;

            return View(viewModel);
        }


        [Authorize(Roles = "Default")] // Authorized only to default user
        [HttpPost]
        public async Task<IActionResult> Rent(RentViewModel rentmodel)
        {

            var user = await _userManager.GetUserAsync(User); // get current user
            var realestate = await _realestateRepository.GetRealestateById(rentmodel.Rent.RealestateId); // get the chosen real estate

            if (user == null || realestate == null)
            {
                _logger.LogError("[RealestateController] realestate or user not found");
                return BadRequest("User or Real estate not found.");
            }

            var newRent = new Rent // create a new rent
            {
                RentDateFrom = rentmodel.Rent.RentDateFrom,
                RentDateTo = rentmodel.Rent.RentDateTo,
                UserId = user.Id,
                User = user,
                RealestateId = realestate.RealestateId,
                Realestate = realestate,
            };

            var days = (rentmodel.Rent.RentDateTo - rentmodel.Rent.RentDateFrom).Days; // Find out the number of days customers want to stay
            newRent.TotalPrice = days * realestate.Price; // Find out price

            realestate.IsDeleted = true; // Mark the Realestate as deleted

            bool returnOK = await _realestateRepository.Rent(newRent);

            if (returnOK)
            {
                return RedirectToAction(nameof(GeneralGrid));
            }

            else
            {
                _logger.LogError("[RealestateController] Rent creation failed");
                return BadRequest("Rent creation failed");
            }

        }

        // Function for uploading file
        public async Task<string> FileUpload()
        {

            IFormFile? file = null;

            if (Request.Form.Files.Count > 0) // if there are any uploaded files
            {  

                file = Request.Form.Files.FirstOrDefault(); // we take it
            }

            if (file != null)
            {
                // Get file name with help of path class.
                var currFileName = Path.GetFileName(file.FileName ?? "");

                // Get the directory name from this new file path
                var myDir = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "uploadedimages", currFileName));

                // If the directory does not exist, we create a new directory
                if (!Directory.Exists(myDir))
                {
                    // Create here
                    if (myDir != null)
                    {
                        Directory.CreateDirectory(myDir);
                    }
                }

                // Copy our image to the indicated path
                try
                {
                    // Create the file, or overwrite if the file exists. Checkout the link: https://learn.microsoft.com/en-us/dotnet/api/system.io.file.create?view=net-6.0 
                    await using var newfile = System.IO.File.Create(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "uploadedimages", currFileName));
                    await file.CopyToAsync(newfile); 
                }
                catch (Exception e)
                {
                    // Log if copying file is wrong
                    _logger.LogError("[RealestateController] Fail while copying {e}", e);
                    return "";
                }
                // Return file path. This will help our realestate.imageurl to navigate to image's location
                return Path.Combine("/", "img", "uploadedimages", currFileName);
            }

            else
            {
                return "";
            }
        }
    }
}

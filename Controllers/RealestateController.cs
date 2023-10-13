using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.Controllers
{
    public class RealestateController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager; // call usermanager
        private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager
        private readonly RealestateDbContext _realestateDbContext;

        public RealestateController(RealestateDbContext realestateDbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _realestateDbContext = realestateDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> GeneralGrid() // this view will return both leilighet and hus
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> GeneralTable() // general table view
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "GeneralTable");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentGrid() // only apartments, grid layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "ApartmentGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> ApartmentTable() // only apartments, table layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "Apartment").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "ApartmentTable");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseGrid() // only houses, grid layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "HouseGrid");
            return View(listmodel);
        }

        public async Task<IActionResult> HouseTable() // only houses, table layout
        {
            List<Realestate> propertylist = await _realestateDbContext.Realestates.Where(p => p.Type == "House").ToListAsync();
            var listmodel = new RealestateListViewModel(propertylist, "HouseTable");
            return View(listmodel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _realestateDbContext.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
            if (item == null)
                return NotFound();
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

            if (user == null) // if no one is logged in
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            else
            {
                if (ModelState.IsValid)
                {
                    _realestateDbContext.Realestates.Add(property); // add to db
                    await _realestateDbContext.SaveChangesAsync(); 
                    return RedirectToAction(nameof(GeneralGrid));
                }
                return View(property);
            }

        }

    }
}

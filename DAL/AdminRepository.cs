using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.DAL;

public class AdminRepository : IAdminRepository
{
    private readonly RealestateDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AdminRepository> _logger; //adding private readonly logger


    public AdminRepository(RealestateDbContext db, UserManager<ApplicationUser> userManager,
            ILogger<AdminRepository> logger)
    {
        _userManager = userManager;
        _db = db;
        _logger = logger; //initialise the logger with the constructor
    }



    // this method is to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
    public IQueryable<Realestate>? GetActiveRealestates()
    {
        try
        {
            return _db.Realestates.Where(r => !r.IsDeleted); // retrieve all active real estate
        }
        catch (Exception e) // logg if there is error
        {
            _logger.LogError("[AdminRepository] An error occurred while retrieving active realestates in GetActiveRealestates(), error message: {e}", e.Message);
            return null;
        }

    }


    public async Task<IEnumerable<ApplicationUser>?> ListAllUsers() // List all active users in our system
    {
        try
        {
            return await _db.User.ToListAsync(); // get users from database
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] user ToListAsync() failed when ListAllUsers(), error message: {e}", e.Message);
            return null;
        }

    }



    public async Task<IEnumerable<Realestate>?> ListAllRealestates() // get all real estate as a list, and these real estate should be "active"
    {
        try
        {
            var activeRealestates = GetActiveRealestates(); // first filter all real estate that match this requirement

            if (activeRealestates != null)
            {
                return await activeRealestates.ToListAsync(); // then we obtain them as list
            }
            else
            {
                // Handle the case where GetActiveRealestates() returns null.
                return null;
            }
        }
        catch (Exception e) // log if there is something wrong
        {
            _logger.LogError("[AdminRepository] activeRealestates ToListAsync() failed when ListAllRealestates(), error message: {e}", e.Message);
            return null;
        }
    }




    public async Task<bool> UpdateRealestate(Realestate realestate) // update real estate by taking an real estate object as argument
    {

        try
        {
            _db.Realestates.Update(realestate); // update this real estate to the newest information
            await _db.SaveChangesAsync(); 
            return true;
        }

        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to update Realestate, error message: {e}", e.Message);
            return false;
        }

    }
    public async Task<bool> UpdateUser(ApplicationUser user) // update information to a specific user
    {
        try
        {
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to update User, error message: {e}", e.Message);
            return false;
        }

    }

    public async Task<bool> DeleteUser(string userid) // delete user based on its id
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userid); // find the user that we want to delete

            if (user == null) // if user is not found, then return false
            {
                return false;
            }


            // if user is existed, we also deleted all properties this user owns
            if (user.Realestate != null)
            {
                foreach (var realestate in user.Realestate) // mark all real estate this user owns and "soft-delete" them
                {
                    realestate.IsDeleted = true;
                }
            }


            var result = await _userManager.DeleteAsync(user); // delete user

            if (result.Succeeded) // return true if our act is going through
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to delete user with ID {UserId}: error message: {e}", userid, e.Message);
            return false;
        }


    }

    public async Task<Realestate?> GetRealestateById(int id) // get a specific real estate based on id
    {
        try
        {
            return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] An error occurred while attempting to retrieve real estate by ID in GetRealestateByID for RealestateID {RealestateId:0000}, error message: {e}", id, e.Message);
            return null;
        }

    }




    public async Task<ApplicationUser?> GetUserById(string id) // get the specific user based on id
    {
        try
        {
            return await _db.User.FindAsync(id); // find user in database
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] User FindAsync(id) failed when GetUserById for User {UserId:0000}, error message: {e}", id, e.Message);
            return null;
        }

    }



}




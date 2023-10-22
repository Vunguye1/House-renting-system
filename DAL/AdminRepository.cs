using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project1.Models;

namespace Project1.DAL;

public class AdminRepository : IAdminRepository
{
    private readonly RealestateDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AdminRepository> _logger; //adding private readonly logger


    public AdminRepository(RealestateDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ILogger<AdminRepository> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
        _logger = logger; //initialise the logger with the constructor
    }



    // this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
    public IQueryable<Realestate>? GetActiveRealestates()
    {
        try
        {
           return _db.Realestates.Where(r => !r.IsDeleted);
        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] An error occurred while retrieving active realestates in GetActiveRealestates(), error message: {e}", e.Message);
            return null;
        }
        
    }


    //Task<IEnumerable<ApplicationUser>> ListAllUsers();
    public async Task<IEnumerable<ApplicationUser>?> ListAllUsers()
    {
        try
        {
            return await _db.User.ToListAsync();
        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] user ToListAsync() failed when ListAllUsers(), error message: {e}", e.Message);
            return null;
        }
        
    }




    //Task<IEnumerable<Realestate>> ListAllRealestates();
    public async Task<IEnumerable<Realestate>?> ListAllRealestates()
    {
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.ToListAsync();
            }
            else
            {
                // Handle the case where GetActiveRealestates() returns null.
                return null;
            }
        }
        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] activeRealestates ToListAsync() failed when ListAllRealestates(), error message: {e}", e.Message);
            return null;
        }
    }




    //Task UpdateRealestate(Realestate realestate);
    public async Task<bool> UpdateRealestate(Realestate realestate)
    {

        try
        {
            _db.Realestates.Update(realestate);
            await _db.SaveChangesAsync();
            return true;
        }

        catch (Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to update Realestate, error message: {e}", e.Message );
            return false;
        }
        
    }


    //Task<bool> Delete(int id);
    public async Task<bool> DeleteRealestate(int id)
    {

        try
        {
            var property = await _db.Realestates.FindAsync(id);
            if (property == null)
            {
                return false;
            }

            _db.Realestates.Remove(property);
            await _db.SaveChangesAsync();
            return true;

        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to delete Realestate with ID {RealestateId}: error message: {e}", id, e.Message);
            return false;
        }
        
    }



    //Task UpdateUser(ApplicationUser user);
    public async Task<bool> UpdateUser(ApplicationUser user)
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


    //Task<bool> DeleteUser(String userid);
    public async Task<bool> DeleteUser(string userid)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userid); // find the user that we want to delete

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] Failed to delete user with ID {UserId}: error message: {e}", userid, e.Message);
            return false;
        }
        

    }

    //DENNE
    //se på loggingen her, hvordan vet den at realestateID i errormeldingen skal byttes ut med id'en???
    public async Task<Realestate?> GetRealestateById(int id)
    {
        try
        {
            return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] An error occurred while attempting to retrieve real estate by ID in GetRealestateByID for RealestateID {RealestateId:0000}, error message: {e}",id, e.Message);
            return null;
        }
        
    }



    //ER DETTE RIKTIG? DEN UNDER?
    public async Task<ApplicationUser?> GetUserById(String id)
    {
        try
        {
            return await _db.User.FindAsync(id);
        }
        catch(Exception e)
        {
            _logger.LogError("[AdminRepository] User FindAsync(id) failed when GetUserById for User {UserId:0000}, error message: {e}", id, e.Message);
            return null;
        }

    }



}




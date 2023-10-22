using System;
using Project1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Project1.DAL;

public class ApplicationUserRepository : IApplicationUserRepository
{
	private readonly RealestateDbContext _db;
	private readonly UserManager<ApplicationUser> _userManager; // call usermanager
	private readonly SignInManager<ApplicationUser> _signInManager; // call signIn manager
	private readonly ILogger<ApplicationUserRepository> _logger; //adding private readonly logger

	public ApplicationUserRepository(RealestateDbContext db, UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager, ILogger<ApplicationUserRepository> logger)
	{
		_db = db;
		_userManager = userManager;
		_signInManager = signInManager;
		_logger = logger;
	}


	public IQueryable<Realestate> GetActiveRealestates()
	{
		try
		{
            return _db.Realestates.Where(r => !r.IsDeleted);
        }
		catch(Exception e)
		{
			_logger.LogError("[ApplicationUserRepository] An error occurred while retrieving active realestates, error message: {e}" , e.Message);
			return null;
		}
		
	}

	public async Task<Realestate?> GetRealestateById(int id)
	{
        try
        {
            return await _db.Realestates.FindAsync(id);
        }
        catch (Exception e)
        {
			_logger.LogError("[ApplicationUserRepository] Realestates ToListAsync() failed when GetRealestateById for RealestateId {RealestateId:0000}, error message: {e}", id, e.Message);
			return null;
        }
        
	}

	public async Task<IEnumerable<Realestate>> GetRealestateByOwner(ApplicationUser user)
	{
        try
        {
            return await GetActiveRealestates().Where(p => p.UserId == user.Id).ToListAsync();
        }
        catch (Exception e)
        {
			_logger.LogError("[ApplicationUserRepository] An error occurred while retrieving realestates by owner, error message: {e}", e.Message);
			return null;
        }
        
	}

	public async Task<IEnumerable<Rent>> ListRentHistory(string userId) {
		try
		{
            return await _db.Rent.Where(p => p.UserId == userId).ToListAsync();
        }
		catch(Exception e)
		{
			_logger.LogError("[ApplicationUserRepository] An error occurred while listing rent history for user with ID {UserId:0000}, error message:  {e} ", userId, e.Message);
			return null;
		}

		
    }


    public async Task<bool> Delete(int id)
	{
		try
		{
            var realestate = await _db.Realestates.FindAsync(id);
            if (realestate == null)
            {
                return false;
            }
            _db.Realestates.Remove(realestate);
            await _db.SaveChangesAsync();
            return true;
        }
		catch(Exception e)
		{
			_logger.LogError("[ApplicationUserRepository] Realestate deletion failed for the RealestateId {RealestateId:0000} , error message: {e}", id, e.Message);
			return false;
		}
		
	}

	public async Task<bool> Update(Realestate realestate)
	{
		try
		{
            _db.Realestates.Update(realestate);
            await _db.SaveChangesAsync();
			return true;
        }

		catch(Exception e)
		{
			_logger.LogError("[ApplicationUserRepository]  An error occurred while updating realestate, error message: {e}", e.Message);
			return false;
		}

	}




}


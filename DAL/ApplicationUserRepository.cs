using System;
using Project1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Project1.DAL;

public class ApplicationUserRepository : IApplicationUserRepository
{
	private readonly RealestateDbContext _db;

	private readonly ILogger<ApplicationUserRepository> _logger; //adding private readonly logger

	public ApplicationUserRepository(RealestateDbContext db, ILogger<ApplicationUserRepository> logger)
	{
		_db = db;
		_logger = logger;
	}


	public IQueryable<Realestate>? GetActiveRealestates() // Query method. Get only real estate that are not marked as "deeted"
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

	public async Task<Realestate?> GetRealestateById(int id) // get real estate based on id
	{
        try
        {
            return await _db.Realestates.FindAsync(id); // find it based on real estate id
        }
        catch (Exception e)
        {
			_logger.LogError("[ApplicationUserRepository] Realestates ToListAsync() failed when GetRealestateById for RealestateId {RealestateId:0000}, error message: {e}", id, e.Message);
			return null;
        }
        
	}

	public async Task<IEnumerable<Realestate>?> GetRealestateByOwner(ApplicationUser user) // Retrieve real estate that user owns
	{
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.Where(p => p.UserId == user.Id).ToListAsync(); // find these real estate by comparing there userid
            }
            else
            {
                // Handle the case where GetActiveRealestates() returns null.
                return null;
            }
           
        }
        catch (Exception e)
        {
			_logger.LogError("[ApplicationUserRepository] An error occurred while retrieving realestates by owner, error message: {e}", e.Message);
			return null;
        }
        
	}

	public async Task<IEnumerable<Rent>?> ListRentHistory(ApplicationUser user) { // list all transaction/rent history
		try
		{
            return await _db.Rent.Where(p => p.UserId == user.Id).ToListAsync(); // getting these data by comparing their userId
        }
		catch(Exception e)
		{
			_logger.LogError("[ApplicationUserRepository] An error occurred while listing rent history for user with ID {UserId:0000}, error message:  {e} ", user.Id, e.Message);
			return null;
		}

		
    }

	public async Task<bool> Update(Realestate realestate) // update the real estate
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


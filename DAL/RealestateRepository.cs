using System;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.DAL;

	public class RealestateRepository : IRealestateRepository
	{
        private readonly RealestateDbContext _db;
        private readonly ILogger<RealestateRepository> _logger;

    
    public RealestateRepository(RealestateDbContext db, ILogger<RealestateRepository> logger)
    {
        _db = db;
        _logger = logger;
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
            _logger.LogError("[RealestateRepository] An error occurred while retrieving active realestates, error message: {e}", e.Message);
            return null;
        }
        
    }

    public async Task<IEnumerable<Realestate>?> GetAll() // get all existing real estate
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

        catch(Exception e)
        {
            _logger.LogError("[RealestateRepository] GetActiveRealestate toListAsync() failed when GetAll(), error message: {e}", e.Message);
            return null;

        }
    }

    public async Task<IEnumerable<Realestate>?> GetOnlyApartment() // get existing apartments in our system
    {
        
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.Where(p => p.Type == "Apartment").ToListAsync(); // filtering by comparing the "key" type-word
            }
            else
            {
                // Handle the case where GetActiveRealestates() returns null.
                return null;
            }
            
        }
        catch(Exception e)
        {
            _logger.LogError("[RealestateRepository] GetActiveRealestates toListAsync() failed when GetOnlyApartment, error message: {e}", e.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Realestate>?> GetOnlyHouse() // get existing houses in our system
    {
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.Where(p => p.Type == "House").ToListAsync(); // filtering by comparing the "key" type-word
            }
            else
            {
                // Handle the case where GetActiveRealestates() returns null.
                return null;
            }
        }
        catch (Exception e)
        {
            _logger.LogError("[RealestateRepository] GetActiveRealestates toListAsync() failed when GetOnlyHouse, error message: {e}", e.Message);
            return null;
        }
        

    }

    public async Task<Realestate?> GetRealestateById(int id) // get existing real esate by id
    {
        try
        {
            return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id); // compare id to get the correct one
        }
        catch(Exception e)
        {
            _logger.LogError("[RealestateRepository] An error occurred while retrieving realestate with ID {RealestateId:0000} , error message: {e}", e.Message, id);
            return null;
        }
       
    }

    public async Task<bool> Create(Realestate property) // add the new real estate to our database
    {
        try
        {
            _db.Realestates.Add(property);
            await _db.SaveChangesAsync();
            return true;
        }

        catch (Exception e)
        {
            _logger.LogError("[RealestateRepository] realestate creation failed for realestate {@property}, error message: {e}", property, e.Message);
            return false;
        }
    }

    public async Task<bool> Rent(Rent nyrent) // add new rent history to data base
    {
        try
        {
            _db.Rent.Add(nyrent);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("[RealestateRepository] An error occurred while attempting to rent a property, error message: {e}", e.Message);
            return false;
        }
    }


    public async Task<bool> DeleteRealestate(int id) // delete real estate based on id
    {

        try
        {
            var property = await _db.Realestates.FindAsync(id); // first, we get access to the right property
            if (property == null) // if it is not existed, return false
            {
                return false;
            }

            _db.Realestates.Remove(property); // remove this property and return true if everything is alright
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception e) // log error
        {
            _logger.LogError("[AdminRepository] Failed to delete Realestate with ID {RealestateId}: error message: {e}", id, e.Message);
            return false;
        }

    }
}


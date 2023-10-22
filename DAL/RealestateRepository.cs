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

    public async Task<IEnumerable<Realestate>?> GetAll()
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

    public async Task<IEnumerable<Realestate>?> GetOnlyApartment()
    {
        
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.Where(p => p.Type == "Apartment").ToListAsync();
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

    public async Task<IEnumerable<Realestate>?> GetOnlyHouse()
    {
        try
        {
            var activeRealestates = GetActiveRealestates();

            if (activeRealestates != null)
            {
                return await activeRealestates.Where(p => p.Type == "House").ToListAsync();
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

    public async Task<Realestate?> GetRealestateById(int id)
    {
        try
        {
            return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
        }
        catch(Exception e)
        {
            _logger.LogError("[RealestateRepository] An error occurred while retrieving realestate with ID {RealestateId:0000} , error message: {e}", e.Message, id);
            return null;
        }
       
    }

    public async Task<bool> Create(Realestate property)
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

    public async Task<bool> Rent(Rent nyrent)
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
}


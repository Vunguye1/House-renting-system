using System;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.DAL;

	public class RealestateRepository : IRealestateRepository
	{
        private readonly RealestateDbContext _db;

    
    public RealestateRepository(RealestateDbContext db)
    {
        _db = db;
    }


    // this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
    public IQueryable<Realestate> GetActiveRealestates()
    {
        return _db.Realestates.Where(r => !r.IsDeleted);
    }

    public async Task<IEnumerable<Realestate>?> GetAll()
    {
        try
        {
            return await GetActiveRealestates().ToListAsync();

        }

        catch(Exception ex)
        {
            return null;

        }
    }

    public async Task<IEnumerable<Realestate>?> GetOnlyApartment()
    {
        return await GetActiveRealestates().Where(p => p.Type == "Apartment").ToListAsync();
    }

    public async Task<IEnumerable<Realestate>?> GetOnlyHouse()
    {
        return await GetActiveRealestates().Where(p => p.Type == "House").ToListAsync();

    }

    public async Task<Realestate?> GetRealestateById(int id)
    {
        return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
    }

    public async Task<bool> Create(Realestate property)
    {
        try
        {
            _db.Realestates.Add(property);
            await _db.SaveChangesAsync();
            return true;
        }

        catch (Exception ex)
        {
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
        catch (Exception ex)
        {
            return false;
        }
    }
}


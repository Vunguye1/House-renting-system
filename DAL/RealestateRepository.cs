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

    public IQueryable<Realestate> GetActiveRealestates()
    {

        return _db.Realestates.Where(r => !r.IsDeleted);
    }

    public async Task Create (Realestate property)
    {
        _db.Realestates.Add(property);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> Rent(RentViewModel rentmodel)
    {

        return true; 
    }









}


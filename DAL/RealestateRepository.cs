using System;
using Project1.Models;

namespace Project1.DAL;

	public class RealestateRepository : IRealestateRepository
	{
        private readonly RealestateDbContext _db;

    
    public RealestateRepository(RealestateDbContext db)
    {
        _db = db;
    }

    public async Task Create (Realestate property)
    {
        _db.Realestates.Add(property);
        await _db.SaveChangesAsync();
    }


}


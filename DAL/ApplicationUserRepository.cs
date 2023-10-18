using System;
using Project1.Models;
using Microsoft.EntityFrameworkCore;

namespace Project1.DAL;

	public class ApplicationUserRepository : IApplicationUserRepository
	{
	private readonly RealestateDbContext _db;

	public ApplicationUserRepository(RealestateDbContext db)
	{
		_db = db;
	}

   
    public IQueryable<Realestate> GetActiveRealestates()
    {
        return _db.Realestates.Where(r => !r.IsDeleted);
    }

	public async Task<Realestate?> GetRealestateById(int id)
	{
		return await _db.Realestates.FindAsync(id);
	}

	public async Task<bool> Delete(int id)
	{
		var realestate = await _db.Realestates.FindAsync(id);
		if(realestate == null)
		{
			return false;
		}
		_db.Realestates.Remove(realestate);
		await _db.SaveChangesAsync();
		return true;
	}

	public async Task Update(Realestate realestate)
	{
		_db.Realestates.Update(realestate);//DENNE
		await _db.SaveChangesAsync();

	}




}


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

	public ApplicationUserRepository(RealestateDbContext db, UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
	{
		_db = db;
		_userManager = userManager;
		_signInManager = signInManager;
	}


	public IQueryable<Realestate> GetActiveRealestates()
	{
		return _db.Realestates.Where(r => !r.IsDeleted);
	}

	public async Task<Realestate?> GetRealestateById(int id)
	{
		return await _db.Realestates.FindAsync(id);
	}

	public async Task<IEnumerable<Realestate>> GetRealestateByOwner(ApplicationUser user)
	{

		return await GetActiveRealestates().Where(p => p.UserId == user.Id).ToListAsync();
	}

	public async Task<IEnumerable<Rent>> ListRentHistory(string userId) {
		return await _db.Rent.Where(p => p.UserId == userId).ToListAsync();
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
			return false;
		}

	}




}


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


    public AdminRepository(RealestateDbContext db, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }



    // this method is to to exclude deleted Realestate records. A real estate is marked as deleted after a customer rent it
    public IQueryable<Realestate> GetActiveRealestates()
    {
        return _db.Realestates.Where(r => !r.IsDeleted);
    }


    //Task<IEnumerable<ApplicationUser>> ListAllUsers();
    public async Task<IEnumerable<ApplicationUser>> ListAllUsers()
    {
        return await _db.User.ToListAsync();
    }




    //Task<IEnumerable<Realestate>> ListAllRealestates();
    public async Task<IEnumerable<Realestate>> ListAllRealestates()
    {
        try
        {
            return await GetActiveRealestates().ToListAsync();

        }

        catch (Exception ex)
        {
            return null;

        }
    }



    //Task UpdateRealestate(Realestate realestate);
    public async Task UpdateRealestate(Realestate realestate)
    {
        _db.Realestates.Update(realestate);
        await _db.SaveChangesAsync();
    }


    //Task<bool> Delete(int id);
    public async Task<bool> DeleteRealestate(int id)
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



    //Task UpdateUser(ApplicationUser user);
    public async Task UpdateUser(ApplicationUser user)
    {
        await _db.SaveChangesAsync();
    }


    //Task<bool> DeleteUser(String userid);
    public async Task<bool> DeleteUser(string userid)
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

    public async Task<Realestate?> GetRealestateById(int id)
    {
        return await _db.Realestates.FirstOrDefaultAsync(i => i.RealestateId == id);
    }



    //ER DETTE RIKTIG? DEN UNDER?
    public async Task<ApplicationUser?> GetUserById(String id)
    {
        return await _db.User.FindAsync(id);

    }



}




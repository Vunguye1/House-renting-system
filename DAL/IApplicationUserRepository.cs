using System;
using System.Threading.Tasks;
using Project1.Models;

namespace Project1.DAL;

public interface IApplicationUserRepository
{
    IQueryable<Realestate>? GetActiveRealestates();
    Task<Realestate?> GetRealestateById(int id);

    Task<IEnumerable<Realestate>?> GetRealestateByOwner(ApplicationUser user);

    Task<IEnumerable<Rent>?> ListRentHistory(ApplicationUser user);

    Task<bool> Delete(int id);
    Task<bool> Update(Realestate realestate);

}



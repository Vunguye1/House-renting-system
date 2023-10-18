using System;
using Project1.Models;

namespace Project1.DAL;

	public interface IApplicationUserRepository
	{
    IQueryable<Realestate> GetActiveRealestates();
	Task<Realestate?> GetRealestateById(int id);
	Task<bool> Delete(int id);
	Task Update(Realestate realestate);

}



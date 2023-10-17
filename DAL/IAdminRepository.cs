using System;
using Project1.Models;
using Project1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Project1.DAL;


	public interface IAdminRepository
	{
	
		IQueryable<Realestate> GetActiveRealestates();
		Task<IEnumerable<ApplicationUser>> ListAllUsers();
		Task<IEnumerable<Realestate>> ListAllRealestates();
		Task UpdateRealestate(Realestate realestate);
		Task<bool> DeleteRealestate(int id);
		Task UpdateUser(ApplicationUser user);
		Task<bool> DeleteUser(string userid);
		Task<Realestate?> GetRealestateById(int id);

		Task<ApplicationUser?> GetUserById(String userId);
		//Task<ApplicationUser?> GetUserByUser(ApplicationUser user);
		



}



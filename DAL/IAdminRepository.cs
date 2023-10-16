using System;
using Project1.Models;
namespace Project1.DAL;

	public interface IAdminRepository
	{
		//LEGGE INN HVIS MÅ:Vet ikke om vi  kan hente getactiverealestates fra realestaterepository


		Task<IEnumerable<ApplicationUser>> ListAllUsers();
		//Kan hende du kan dropåpe den under også bruke den fra realestatertepository
		Task<IEnumerable<Realestate>> ListAllRealestates();
		Task UpdateRealestate(Realestate realestate);
		Task<bool> Delete(int id);
		Task UpdateUser(ApplicationUser user);
		Task<bool> DeleteUser(String userid);



}



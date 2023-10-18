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






}


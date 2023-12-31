﻿using System;
using Project1.Models;
using Project1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Project1.DAL;


public interface IAdminRepository
{

    IQueryable<Realestate>? GetActiveRealestates();
    Task<IEnumerable<ApplicationUser>?> ListAllUsers();
    Task<IEnumerable<Realestate>?> ListAllRealestates(); 
    Task<bool> UpdateRealestate(Realestate realestate);
    Task<bool> UpdateUser(ApplicationUser user);
    Task<bool> DeleteUser(string userid);
    Task<Realestate?> GetRealestateById(int id);

    Task<ApplicationUser?> GetUserById(String userId);




}



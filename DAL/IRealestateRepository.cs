using System;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.DAL;

public interface IRealestateRepository
{

    IQueryable<Realestate>? GetActiveRealestates();

    Task<IEnumerable<Realestate>?> GetAll();

    Task<IEnumerable<Realestate>?> GetOnlyHouse();

    Task<IEnumerable<Realestate>?> GetOnlyApartment();

    Task<Realestate?> GetRealestateById(int id);

    Task<bool> Create(Realestate property);


    Task<bool> DeleteRealestate(int id);

    Task<bool> Rent(Rent nyrent);

}






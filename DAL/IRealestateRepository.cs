using System;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.DAL;

public interface IRealestateRepository
{

    IQueryable<Realestate> GetActiveRealestates();

    Task<IEnumerable<Realestate>?> GetAll();

    Task<IEnumerable<Realestate>?> GetOnlyHouse();

    Task<IEnumerable<Realestate>?> GetOnlyApartment();

    Task<Realestate?> GetRealestateById(int id);

    Task<bool> Create(Realestate property);

    //public async Task<IActionResult> Rent(RentViewModel rentmodel)
    Task<bool> Rent(Rent nyrent);

}







//public async Task<IActionResult> GeneralGrid()
//public async Task<IActionResult> GeneralTable()
//public async Task<IActionResult> ApartmentGrid()
//public async Task<IActionResult> ApartmentTable()
//public async Task<IActionResult> HouseGrid()
//public async Task<IActionResult> HouseTable()

//public async Task<IActionResult> Details(int id)

//public async Task<IActionResult> Rent(int realestateId)
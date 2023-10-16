using System;
using Microsoft.AspNetCore.Mvc;
using Project1.Models;
using Project1.ViewModels;

namespace Project1.DAL;

	public interface IRealestateRepository
	{
        //public IQueryable<Realestate> GetActiveRealestates() 
        IQueryable<Realestate> GetActiveRealestates();
        // public async Task<IActionResult> Create(Realestate property)
        Task Create(Realestate property);
        //public async Task<IActionResult> Rent(RentViewModel rentmodel)
        Task<bool> Rent(RentViewModel rentmodel);

    }







//public async Task<IActionResult> GeneralGrid()
//public async Task<IActionResult> GeneralTable()
//public async Task<IActionResult> ApartmentGrid()
//public async Task<IActionResult> ApartmentTable()
//public async Task<IActionResult> HouseGrid()
//public async Task<IActionResult> HouseTable()

//public async Task<IActionResult> Details(int id)

//public async Task<IActionResult> Rent(int realestateId)
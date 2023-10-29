using Microsoft.AspNetCore.Mvc;
using Project1.Models;

namespace Project1.ViewModels
{
    public class RentViewModel // this model is made to display the name of the real estate user want to book
    {
        public Realestate Realestate { get; set; } = default!;
        public Rent Rent { get; set; } = default!;


    }
}

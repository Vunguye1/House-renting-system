using System;
namespace Project1.Models
{
	public class Rent
	{
		public int RentID { get; set; }
		public string RentDateFrom { get; set; } = string.Empty;
		public string RentDateTo { get; set; } = string.Empty;
		public int UserId { get; set; }
		public int RealestateId { get; set; }
		//navigation property
		public Realestate Realestate { get; set; } = default!;
		public decimal TotalPrice { get; set; }
		//navigation property
		public User User { get; set; } = default!;
	}
}


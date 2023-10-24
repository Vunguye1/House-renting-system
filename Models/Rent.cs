using System;
namespace Project1.Models
{
	public class Rent
	{
		public int RentID { get; set; }

		public DateTime RentDateFrom { get; set; }
		public DateTime RentDateTo { get; set; }

        public string? UserId { get; set; } = string.Empty;
        //navigation property
        public virtual ApplicationUser? User { get; set; } = default!;

        public int RealestateId { get; set; }
        //navigation property
        public virtual Realestate? Realestate { get; set; } = default!;


		public decimal TotalPrice { get; set; }
		
	}
}


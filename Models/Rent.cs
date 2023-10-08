using System;
namespace Project1.Models
{
	public class Rent
	{
		public int RentID { get; set; }
		public string RentDateFrom { get; set; } = string.Empty;
		public string RentDateTo { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        //navigation property
        public virtual ApplicationUser User { get; set; } = default!;

        public int RealestateId { get; set; }
        //navigation property
        public virtual Realestate Realestate { get; set; } = default!;

		public decimal TotalPrice { get; set; }
		
	}
}


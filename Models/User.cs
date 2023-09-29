using System;
namespace Project1.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Adress { get; set; } = string.Empty;
		//navigation property
		public List<Realestate>? Realestate { get; set; }
	}
}


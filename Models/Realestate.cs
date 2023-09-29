namespace Project1.Models
{
    public class Realestate
    {
        public int RealestateId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? imageurl { get; set; }
        public string? imagefile { get; set; }
        public int? Persons { get; set; }
        public int? Bathrooms { get; set; }
        //navigation property
        public virtual List<Rent>? Rents { get; set; }
        
    }
}

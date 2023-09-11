namespace Project1.Models
{
    public class Property
    {
        public int PropertyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? imageurl { get; set; }
        
    }
}

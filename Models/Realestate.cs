using Castle.Core.Resource;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Realestate
    {

        [Key]
        public int RealestateId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public string? imageurl { get; set; }
        public string? imagefile { get; set; }
        public int? Persons { get; set; }
        public int? Bathrooms { get; set; }

        public string? UserId { get; set; }

        //navigation property
        public virtual ApplicationUser? User { get; set; }
        
    }
}

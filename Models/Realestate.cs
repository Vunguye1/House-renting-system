using Castle.Core.Resource;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Realestate
    {

        [Key]
        public int RealestateId { get; set; }

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage="The name must be numbers or letters and between 2 to 20 characters.")]
        public string Name { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
        public int Price { get; set; }


        [RegularExpression(@"[a-zA-ZæøåÆØÅ., \-]{2,50}", ErrorMessage = "Location must must consist of 2 to 50 letters")]
        public string Location { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        public bool IsDeleted { get; set; }
        public string? imageurl { get; set; }
        public string? imagefile { get; set; }

        [Range(1, 10, ErrorMessage = "Persons must be between 1 and 10.")]
        public int Persons { get; set; }

        [Range(0, 10, ErrorMessage = "Bathrooms must be between 0 and 10.")]
        public int Bathrooms { get; set; }

        public string? UserId { get; set; }

        //navigation property
        public virtual ApplicationUser? User { get; set; }
        
    }
}

using Microsoft.AspNetCore.Identity;


namespace Project1.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }

        //navigation property
        public virtual List<Realestate>? Realestate { get; set; }
    }
}

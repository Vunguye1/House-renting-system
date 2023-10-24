using Microsoft.AspNetCore.Identity;


namespace Project1.Models
{
    public class ApplicationUser: IdentityUser
    {
        /* 
         We want to add more details to user, and therefore we use this source as inspiration to our code:
        https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/
         */
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }

        //navigation property
        public virtual List<Realestate>? Realestate { get; set; }

        //navigation property
        public virtual List<Rent>? Rents { get; set; }
    }
}

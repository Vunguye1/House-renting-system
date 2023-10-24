using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations; //to be able to do input validation


namespace Project1.Models
{
    public class ApplicationUser: IdentityUser
    {
        /* 
         We want to add more details to user, and therefore we use this source as inspiration to our code:
        https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/
         */

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,}", ErrorMessage = "First name must be letters and be at least 2 characters long.")]
        public string FirstName { get; set; } = string.Empty;

        [RegularExpression(@"[a-zA-ZæøåÆØÅ. \-]{2,}", ErrorMessage = "Last name must be letters and be at least 2 characters long.")]
        public string LastName { get; set; } = string.Empty;

        [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,50}", ErrorMessage = "Address must consist of 2 to 50 letters or numbers.")]
        public string Adress { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }

        //navigation property
        public virtual List<Realestate>? Realestate { get; set; }

        //navigation property
        public virtual List<Rent>? Rents { get; set; }
    }
}

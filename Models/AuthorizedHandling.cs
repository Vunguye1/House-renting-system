using Microsoft.AspNetCore.Identity;
using System;
using Project1.Models;
using System.Data;

namespace Project1.Models
{
    public class AuthorizedHandling
    {
        /* 
         Based on this link, we learn how to handle authorization https://codewithmukesh.com/blog/user-management-in-aspnet-core-mvc/
         */

        //Seed Roles
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create 2 roles. "admin" roles for admin and "default" for normal users
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Default"));


        }


        // Lets create an "admin" user whenever we start application

        public static async Task CreateAdmin(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            // Provide basic info to this admin user
            var adminuser = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "admin",
                LastName = "is_here",
                EmailConfirmed = true,
            };

            // Check if admin user is existed in db
            if (userManager.Users.All(u => u.Id != adminuser.Id))
            {
                var user = await userManager.FindByEmailAsync(adminuser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminuser, "Admin123**");
                    // and add "admin" role to this user
                    await userManager.AddToRoleAsync(adminuser, "Admin");
                }

            }
        }

        
    }
}

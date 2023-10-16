using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RealestateDbContext context = serviceScope.ServiceProvider.GetRequiredService<RealestateDbContext>();
            //context.Database.EnsureDeleted(); //Always deletes the database, can comment out.
            context.Database.EnsureCreated();

            if (!context.Realestates.Any())
            {

                var users = new List<ApplicationUser> {

                    new ApplicationUser
                    {
                        UserName = "peertrust@gmail.com",
                        Email = "peertrust@gmail.com",
                        FirstName = "peer",
                        LastName = "trust",
                        PasswordHash = "Dontknowyet123*",
                        EmailConfirmed = true,
                    },

                    new ApplicationUser
                    {
                        UserName = "randaoili@gmail.com",
                        Email = "randaoili@gmail.com",
                        FirstName = "rand",
                        LastName = "aoili",
                        PasswordHash = "Dontknowyet123*",
                        EmailConfirmed = true,
                    },
                };



                var items = new List<Realestate>

            {

                new Realestate
                {
                    Name = "Studio Apartment",
                    Type="Apartment",
                    Price = 3000,
                    Location="Majorstuen, Oslo",
                    Description = "Beautiful studio apartment at Marjorstuen. Close to public transport and all the hottest stores.",
                    imageurl = "/img/StudioApartment/mainroom.jpg",
                    imagefile="/img/StudioApartment",
                    Persons=4,
                    Bathrooms=1,
                    UserId = users[0].Id,
                    User = users[0]
                },
                 new Realestate
                {
                    Name = "Villa",
                    Type="House",
                    Price = 15000,
                    Location="Bygdøy",
                    Description = "Big Norwegian Villa right outisde Oslo, at Bygdøy. Private pool and beautiful interior.",
                    imageurl = "/img/Villa/pool1.jpg",
                    imagefile = "/img/Villa",
                    Persons= 8,
                    Bathrooms=2,
                    UserId = users[0].Id,
                    User = users[0]
                },
                  new Realestate
                {
                    Name = "Scandinavian tree inspired Home",
                    Type="House",
                    Price = 8000,
                    Location="Grefsen, Oslo",
                    Description = "Big house on the top of Grefsen with amazing views. Just a small trip with the tram down to the city center. Very beautiful and " +
                    "scandinavian inspired with lots of wood used as interior..",
                    imageurl = "/img/house2/outside.jpg",
                    imagefile = "/img/house2",
                    Persons=6,
                    Bathrooms=2,
                    UserId = users[1].Id,
                    User = users[1]
                },



                  // change picture and image file
                  new Realestate
                {
                    Name = "Modern Oasis in the Hills",
                    Type = "House",
                    Price = 2000,
                    Location="Sagene, Oslo",
                    Description = "Discover this contemporary gem nestled in the hills, offering a tranquil escape from the city bustle. Embrace clean lines and stylish architecture.",
                    imageurl = "/img/house3/outside.jpg",
                    imagefile = "/img/house3",
                    Persons=4,
                    Bathrooms=3,
                    UserId = users[0].Id,
                    User = users[0]
                }
                  ,
                  new Realestate
                {
                    Name = "Riverside Retreat",
                    Type="Apartment",
                    Price = 3000,
                    Location="Storo, Oslo",
                    Description = "This charming riverside house is the perfect sanctuary for nature lovers. Enjoy the soothing sound of the river as you relax on the deck.",
                    imageurl = "/img/hus1/outside.jpg",
                    imagefile = "/img/hus1",
                    Persons=5,
                    Bathrooms=2,
                    UserId = users[1].Id,
                    User = users[1]
                }
                  ,
                  new Realestate
                {
                    Name = "Artistic Loft in the Warehouse District",
                    Type="Apartment",
                    Price = 4000,
                    Location="Nydalen, Oslo",
                    Description = "Live in an artist's haven within the trendy warehouse district. This spacious loft is a canvas for creativity, boasting industrial aesthetics and ample space.",
                    imageurl = "/img/Funkyhome/hall.jpg",
                    imagefile = "/img/Funkyhome",
                    Persons=2,
                    Bathrooms=1,
                    UserId = users[1].Id,
                    User = users[1]
                }
                  ,
                  new Realestate
                {
                    Name = "Mediterranean Villa with Pool",
                    Type= "Apartment",
                    Price = 8000,
                    Location="Grefsen, Oslo",
                    Description = "Experience Mediterranean living in this stunning villa. Enjoy the pool, " +
                    "lush gardens, and spacious interiors in this sunny paradise",
                    imageurl = "/img/apartment2/living.jpg",
                    imagefile = "/img/apartment2",
                    Persons=8,
                    Bathrooms=4,
                    UserId = users[1].Id,
                    User = users[1]
                }
                  ,
                  new Realestate
                {
                    Name = "Seaside Cottage Escape",
                    Type="House",
                    Price = 8000,
                    Location="Bjorvika, Oslo",
                    Description = "Unwind in this charming seaside cottage, just steps from the shore. " +
                    "Coastal living at its finest, with a cozy ambiance and ocean views",
                    imageurl = "/img/apartment1/livingroom.jpg",
                    imagefile = "/img/apartment1",
                    Persons=4,
                    Bathrooms=1,
                    UserId = users[0].Id,
                    User = users[0]
                },
            };
                context.AddRange(users);
                context.AddRange(items);
                context.SaveChanges();
            }


            context.SaveChanges();
        }
    }
}


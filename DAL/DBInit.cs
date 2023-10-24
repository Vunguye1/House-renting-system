using Castle.Core.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class DBInit
    {
        public static void Seed(IApplicationBuilder app, UserManager<ApplicationUser> userManager)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            RealestateDbContext context = serviceScope.ServiceProvider.GetRequiredService<RealestateDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            

            if (!context.Realestates.Any())
            {
                // we create two default users here
                var users = new List<ApplicationUser> {

                    new ApplicationUser
                    {
                        UserName = "peertrust@gmail.com",
                        Email = "peertrust@gmail.com",
                        FirstName = "Per",
                        LastName = "Hovland",
                        EmailConfirmed = true,
                    },

                    new ApplicationUser
                    {
                        UserName = "randaoili@gmail.com",
                        Email = "randaoili@gmail.com",
                        FirstName = "Rolf",
                        LastName = "Amundsen",
                        EmailConfirmed = true,
                    }
                };

                // create and give the "default user" role
                userManager.CreateAsync(users[0], "Dontknowyet123*");
                userManager.AddToRoleAsync(users[0], "Default");

                userManager.CreateAsync(users[1], "Dontknowyet123*");
                userManager.AddToRoleAsync(users[1], "Default");



                // Mock some real estates
                var items = new List<Realestate>

            {

                new Realestate
                {
                    Name = "Studio Apartment",
                    Type="Apartment",
                    Price = 3000,
                    Location="Majorstuen, Oslo",
                    Description = "Welcome to our charming studio apartment, nestled in the heart of Majorstua, one of Oslo's most vibrant and trendy neighborhoods. This cozy studio offers everything you need for an unforgettable stay in the capital. The modern and tastefully decorated space features a comfortable sleeping area, a well-equipped kitchenette, and a recently renovated bathroom. However, the true gem of this apartment is its location. Surrounded by cafes, restaurants, shops, and cultural attractions, you'll be perfectly situated to explore the city. After a day of adventures, unwind on the intimate balcony with city views, or savor a cup of coffee while planning your next excursion. This apartment provides the ideal home base for experiencing Oslo at its finest.",
                    imageurl = "/img/StudioApartment/img1.jpg",
                    imagefile="img/StudioApartment",
                    Persons=4,
                    Bathrooms=1,
                    UserId = users[0].Id
                },
                 new Realestate
                {
                    Name = "Villa",
                    Type="House",
                    Price = 15000,
                    Location="Bygdøy",
                    Description = "Welcome to our exquisite Norwegian villa, located on the picturesque Bygdøy Peninsula, a short drive from Oslo. This spacious villa is a true gem, featuring a unique and vibrant interior palette with sunny yellow hues gracing the kitchen and living area. Step inside, and you'll be greeted by the warmth of the sun-soaked spaces that instantly put you at ease. The villa boasts a private pool that's perfect for taking a refreshing dip on warm summer days, while the beautifully manicured garden invites you to relax and soak in the serene surroundings. With its luxurious and cheerful decor, this villa offers a one-of-a-kind retreat just minutes away from the city. Your stay here promises a blend of comfort, style, and a touch of Norwegian charm.",
                    imageurl = "/img/Villa/img1.jpg",
                    imagefile = "img/Villa",
                    Persons= 8,
                    Bathrooms=2,
                    UserId = users[1].Id
                },
                  new Realestate
                {
                    Name = "Scandinavian tree inspired Home",
                    Type="House",
                    Price = 8000,
                    Location="Grefsen, Oslo",
                    Description = "Discover our exceptional Scandinavian tree-inspired home perched atop the stunning Grefsen hill, offering breathtaking panoramic views. A short tram ride takes you down to the vibrant city center, making it the perfect location to explore Oslo. This charming home is a celebration of Scandinavian design, boasting an abundance of wood throughout its interior that evokes a sense of natural serenity. Every corner is thoughtfully crafted, radiating an inviting and cozy atmosphere. The fusion of modern comfort and timeless Nordic aesthetics sets the stage for an unforgettable stay. Embrace the tranquility of this haven, where the beauty of Norway's natural surroundings seamlessly blends with the convenience of urban living.",
                    imageurl = "/img/house2/img1.jpg",
                    imagefile = "img/house2",
                    Persons=6,
                    Bathrooms=2,
                    UserId = users[0].Id
                },



                  // change picture and image file
                  new Realestate
                {
                    Name = "Modern Oasis in the Hills",
                    Type = "House",
                    Price = 2000,
                    Location="Sagene, Oslo",
                    Description = "Unveil a contemporary gem perched high in the hills of Holmenkollen, providing a tranquil escape from the city's hustle and bustle. This architectural masterpiece invites you to indulge in sleek lines and modern design, where form meets function in a perfect union. The house is an oasis of style and sophistication, set against the backdrop of Norway's natural beauty. Revel in the seamless blend of sleek, minimalist aesthetics and the pristine surroundings. This is the ultimate destination to savor the harmony of modern living in a serene, hillside setting.",
                    imageurl = "/img/house3/img1.jpg",
                    imagefile = "img/house3",
                    Persons=4,
                    Bathrooms=3,
                    UserId = users[0].Id
                }
                  ,
                  new Realestate
                {
                    Name = "Riverside Retreat",
                    Type="House",
                    Price = 3000,
                    Location="Storo, Oslo",
                    Description = "Welcome to your Riverside Retreat at Storo, where nature and modern comfort coexist in perfect harmony. This idyllic retreat offers a serene escape from the urban hustle and bustle, nestled along the tranquil riverbanks. The air is filled with the soothing sounds of flowing water, and the views are nothing short of enchanting. Your haven boasts a stylish interior that seamlessly marries contemporary design with the surrounding natural beauty. Here, you can unwind, reflect, and recharge in a space where the river's embrace is your constant companion. Experience a riverside getaway that promises tranquility and rejuvenation, all in the heart of Storo",
                    imageurl = "/img/House/img1.jpg",
                    imagefile = "img/House",
                    Persons=5,
                    Bathrooms=2,
                    UserId = users[0].Id
                }
                  ,
                  new Realestate
                {
                    Name = "Artistic Loft in the Warehouse District",
                    Type="Apartment",
                    Price = 4000,
                    Location="Nydalen, Oslo",
                    Description = "Step into an artistic haven within the heart of Nydalen's trendy warehouse district. This sprawling loft is a canvas for creativity, a true reflection of industrial chic aesthetics and abundant space. Live in a space that fosters imagination and individuality, where the fusion of raw, industrial elements with modern design creates an environment that's both unique and inspiring. This loft transcends the ordinary, offering a dynamic urban living experience for those who appreciate the eclectic charm of warehouse living in one of Oslo's most vibrant neighborhoods.\n\n",
                    imageurl = "/img/Funkyhome/img1.jpg",
                    imagefile = "img/Funkyhome",
                    Persons=2,
                    Bathrooms=1,
                    UserId = users[1].Id
                }
                  ,
                  new Realestate
                {
                    Name = "Frogner Nordic Elegance",
                    Type= "Apartment",
                    Price = 8000,
                    Location="Frogner, Oslo",
                    Description = "Welcome to 'Frogner Nordic Elegance,' a distinctive Scandinavian haven in the heart of Oslo's prestigious Frogner district. This exquisite apartment is a showcase of modern refinement, artfully combining the timeless allure of Nordic design with contemporary sophistication. With a minimalist approach, the interior exudes an air of understated luxury, featuring clean lines, natural materials, and a subdued color palette that harmonize effortlessly. Nestled in one of Oslo's most sought-after neighborhoods, this apartment is the ideal retreat for those who seek both comfort and an elevated living experience. Immerse yourself in the ambiance of 'Frogner Nordic Elegance,' where style and substance converge.e",
                    imageurl = "/img/apartment2/img1.jpg",
                    imagefile = "img/apartment2",
                    Persons=8,
                    Bathrooms=4,
                    UserId = users[1].Id
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
                    imageurl = "/img/apartment1/img1.jpg",
                    imagefile = "img/apartment1",
                    Persons=4,
                    Bathrooms=1,
                    UserId = users[0].Id
                },
            };
                context.AddRange(items);
            }


            context.SaveChanges();
        }
    }
}


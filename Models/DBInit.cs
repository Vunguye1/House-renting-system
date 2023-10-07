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
            // context.Database.EnsureDeleted(); //Always deletes the database, can comment out.
            context.Database.EnsureCreated();

            if (!context.Realestates.Any())
            {
                var items = new List<Realestate>
                
            {

                new Realestate
                {
                    Name = "Studio Apartment",
                    Type="Apartment",
                    Price = 3000,
                    Location="Majorstuen, Oslo",
                    Description = "Beautiful StudioApartment at Marjorstuen. Close to public transport and all the hottest stores.",
                    imageurl = "/img/StudioApartment/mainroom.jpg",
                    imagefile="/img/StudioApartment",
                    Persons=2,
                    Bathrooms=1,
                
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
                    Bathrooms=2
                },
                  new Realestate
                {
                    Name = "Scandinavian tree inspired Home",
                    Type="House",
                    Price = 8000,
                    Location="Grefsen, Oslo",
                    Description = "Big house on the top of Grefsen with amazing views. Just a small trip with the tram down to the city center. Very beautiful and " +
                    "scandinavian inspired with lots of wood used as interior..\nPersons: 6\nBathroom:2",
                    imageurl = "/img/house2/outside.jpg",
                    imagefile = "/img/house2",
                    Persons=6,
                    Bathrooms=2
                }

               
            };
                context.AddRange(items);
                context.SaveChanges();
            }

           
            context.SaveChanges();
        }
    }
}


/*
 * using Microsoft.EntityFrameworkCore;

namespace MyShop.Models;

public static class DBInit
{
    public static void Seed(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ItemDbContext context = serviceScope.ServiceProvider.GetRequiredService<ItemDbContext>();
        context.Database.EnsureDeleted(); //Always deletes the database, can comment out.
        context.Database.EnsureCreated();

        if (!context.Items.Any())
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Pizza",
                    Price = 150,
                    Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
                    ImageUrl = "/images/pizza.jpg"
                },
                new Item
                {
                    Name = "Fried Chicken Leg",
                    Price = 20,
                    Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
                    ImageUrl = "/images/chickenleg.jpg"
                },
                new Item
                {
                    Name = "French Fries",
                    Price = 50,
                    Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
                    ImageUrl = "/images/frenchfries.jpg"
                },
                new Item
                {
                    Name = "Grilled Ribs",
                    Price = 250,
                    Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
                    ImageUrl = "/images/ribs.jpg"
                },
                new Item
                {
                    Name = "Tacos",
                    Price = 150,
                    Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
                    ImageUrl = "/images/tacos.jpg"
                },
                new Item
                {
                    Name = "Fish and Chips",
                    Price = 180,
                    Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
                    ImageUrl = "/images/fishandchips.jpg"
                },
                new Item
                {
                    Name = "Cider",
                    Price = 50,
                    Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
                    ImageUrl = "/images/cider.jpg"
                },
                new Item
                {
                    Name = "Coke",
                    Price = 30,
                    Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
                    ImageUrl = "/images/coke.jpg"
                },
            };
            context.AddRange(items);
            context.SaveChanges();
        }

        if (!context.Customers.Any())
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Alice Hansen", Address = "Osloveien 1"},
                new Customer { Name = "Bob Johansen", Address = "Oslomet gata 2"},
                new Customer { Name = "Serina Erzengin", Address = "Pilestredet 35"},
            };
            context.AddRange(customers);
            context.SaveChanges();
        }

        if (!context.Orders.Any())
        {
            var orders = new List<Order>
            {
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 1,},
                new Order {OrderDate = DateTime.Today.ToString(), CustomerId = 2,},
            };
            context.AddRange(orders);
            context.SaveChanges();
        }

        if (!context.OrderItems.Any())
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { ItemId = 1, Quantity = 2, OrderId = 1},
                new OrderItem { ItemId = 2, Quantity = 1, OrderId = 1},
                new OrderItem { ItemId = 3, Quantity = 4, OrderId = 2},
            };
            foreach (var orderItem in orderItems)
            {
                var item = context.Items.Find(orderItem.ItemId);
                orderItem.OrderItemPrice = orderItem.Quantity * item?.Price ?? 0;
            }
            context.AddRange(orderItems);
            context.SaveChanges();
        }

        var ordersToUpdate = context.Orders.Include(o => o.OrderItems); //Eager loading, OrderItems will be loaded aswell
        foreach (var order in ordersToUpdate)
        {
            order.TotalPrice = order.OrderItems?.Sum(oi => oi.OrderItemPrice) ?? 0;
        }
        context.SaveChanges();
    }
}
 */

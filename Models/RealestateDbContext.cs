using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class RealestateDbContext: DbContext
    {
        public RealestateDbContext(DbContextOptions<RealestateDbContext> options) : base(options) { 
            //Database.EnsureCreated();
        }

        public DbSet<Realestate> Realestates { get; set; } 
    }
}

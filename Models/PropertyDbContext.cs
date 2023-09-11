using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class PropertyDbContext: DbContext
    {
        public PropertyDbContext(DbContextOptions<PropertyDbContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        public DbSet<Property> Properties { get; set; } 
    }
}

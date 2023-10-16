using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project1.Models
{
    public class RealestateDbContext: IdentityDbContext<ApplicationUser>
    {
        public RealestateDbContext(DbContextOptions<RealestateDbContext> options) : base(options) { 
            //Database.EnsureCreated();
        }

        public DbSet<Realestate> Realestates { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Rent> Rent { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Project1.Models
{
    public class RealestateDbContext : IdentityDbContext<ApplicationUser>
    {
        public RealestateDbContext(DbContextOptions<RealestateDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Realestate> Realestates { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Rent> Rent { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // lazy loading handling
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder) // Handling delete behavior when we delete user or real estate
        {

            builder.Entity<Realestate>().HasOne(r => r.User).WithMany(u => u.Realestate
            ).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull); // they will automatically set the value as null

            builder.Entity<Rent>().HasOne(r => r.User).WithMany(u => u.Rents
            ).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull); // they will automatically set the value as null
            base.OnModelCreating(builder);
        }
    }
}

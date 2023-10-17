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

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Realestate>().HasOne(r => r.User).WithMany(u => u.Realestate
            ).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Rent>().HasOne(r => r.User).WithMany(u => u.Rents
            ).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.SetNull);
            base.OnModelCreating(builder);
        }
    }
}

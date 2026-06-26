using DSM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSM.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext
        (
            DbContextOptions<ApplicationDbContext> options
        )
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<BookingRequest> BookingRequests
        { get; set; }


        protected override void OnModelCreating
        (
            ModelBuilder modelBuilder
        )
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<Service>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            

            modelBuilder.Entity<BookingRequest>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);
        }
    }
}
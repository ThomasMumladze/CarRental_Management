using CarRental_Management.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarRental_Management.Data
{
    public class CarRentalDbContext : DbContext
    {
       public DbSet<Car> Cars { get; set; }
       public DbSet<Customer> Customers { get; set; }
       public DbSet<Rental> Rentals{ get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=CarRental_Management;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("car_rental");
        }
    }
}

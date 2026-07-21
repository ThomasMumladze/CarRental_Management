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
            optionsBuilder.UseSqlServer("");
        }
    }
}

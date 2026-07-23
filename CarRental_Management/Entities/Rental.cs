using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Management.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public RentalStatus Status { get; set; }
    }

}

using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental_Management.Entities
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public int CarId { get; set; }

        public Car Car { get; set; } = new Car();

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = new Customer();

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? ActualReturnDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public RentalStatus Status { get; set; }

        
        public bool IsPeriodValid()
        {
            return EndDate > StartDate;
        }


        public int GetRentalDurationInDays()
        {
            return (EndDate.Date - StartDate.Date).Days;
        }

        public bool IsOverdue()
        {
            return Status == RentalStatus.Active && DateTime.Today > EndDate.Date;
        }

        public static decimal CalculateTotalPrice(DateTime startDate, DateTime endDate, decimal dailyRate)
        {
            var days = Math.Max(1, (endDate.Date - startDate.Date).Days);
            return days * dailyRate;
        }

        public void ReturnCar(DateTime returnDate)
        {
            ActualReturnDate = returnDate;
            Status = RentalStatus.Completed;
        }
    }

}

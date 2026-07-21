using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRental_Management.Entities
{
    internal class Rental
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Brand { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Model { get; set; } = string.Empty;
        [Required]
        public int Year { get; set; }
        [Required, MaxLength(50)]
        public string LicensePlate { get; set; } = string.Empty;
        [Required]
        public decimal DailyPrice { get; set; } = decimal.MaxValue;
        public CarCategory Category { get; set; }
        public TransmissionType Transmission { get; set; }
        public FuelType FuelType { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}

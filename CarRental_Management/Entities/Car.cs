using CarRental_Management.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarRental_Management.Entities
{
    internal class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(1950, 2100, ErrorMessage = "incorrect year")]
        public int Year { get; set; }

        [Required]
        [StringLength(15)]
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "daily price should be positive")]
        public decimal DailyPrice { get; set; }

        [Required]
        public CarCategory Category { get; set; }

        [Required]
        public TransmissionType Transmission { get; set; }

        [Required]
        public FuelType FuelType { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();


        public int GetCarAge()
        {
            return DateTime.Today.Year - Year;
        }

        public string GetDisplayName()
        {
            return $"{Brand} {Model} ({Year})";
        }
    }
}

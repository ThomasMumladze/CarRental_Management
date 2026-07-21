using CarRental_Management.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarRental_Management.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public decimal DailyPrice { get; set; }

        public CarCategory Category { get; set; }

        public TransmissionType Transmission { get; set; }
        public FuelType FuelType { get; set; }
        public bool IsAvailable { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();
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

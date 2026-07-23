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
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public decimal DailyPrice { get; set; }
        public CarCategory Category { get; set; }
        public TransmissionType Transmission { get; set; }
        public FuelType FuelType { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation property — ერთ მანქანას ბევრი Rental შეიძლება ჰქონდეს
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

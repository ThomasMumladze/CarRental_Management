using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRental_Management.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string DrivingLicenseNumber { get; set; }
        public DateTime DrivingLicenseExpiration { get; set; }
        public DateTime BirthDate { get; set; }

        // Navigation property — ერთ მომხმარებელს ბევრი Rental შეიძლება ჰქონდეს
        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}

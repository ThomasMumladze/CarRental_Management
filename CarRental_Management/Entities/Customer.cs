using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRental_Management.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PersonalNumber { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string DrivingLicenseNumber { get; set; } = string.Empty;

        public DrivingLicenseCategory DrivingLicenseCategories { get; set; }

        public DateTime DrivingLicenseExpiration { get; set; }

        public DateTime BirthDate { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();

        public bool IsDrivingLicenseValid()
        {
            return DrivingLicenseExpiration.Date >= DateTime.Today;
        }

        public bool HasCategory(DrivingLicenseCategory category)
        {
            return (DrivingLicenseCategories & category) == category;
        }

        public int GetAge()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;
            if (BirthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}

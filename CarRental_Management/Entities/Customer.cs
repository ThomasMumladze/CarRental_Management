using CarRental_Management.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarRental_Management.Entities
{
    internal class Customer
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string PersonalNumber { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string DrivingLicenseNumber { get; set; } = string.Empty;

        [Required]
        public DrivingLicenseCategory DrivingLicenseCategories { get; set; }

        [Required]
        public DateTime DrivingLicenseExpiration { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

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

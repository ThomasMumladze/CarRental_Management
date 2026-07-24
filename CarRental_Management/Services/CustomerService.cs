using CarRental_Management.Entities;
using CarRental_Management.Repositories;

namespace CarRental_Management.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        public CustomerService(ICustomerRepository customerRepository, IRentalRepository rentalRepository)
        {
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }

        // add customer
        public (bool Success, string Message) AddCustomer(
        string firstName, string lastName, string personalNumber,
        string phoneNumber, string drivingLicenseNumber,
        DateTime drivingLicenseExpiration, DateTime birthDate)
            {
                if (string.IsNullOrWhiteSpace(firstName))
                    return (false, "სახელი არ უნდა იყოს ცარიელი.");

                if (string.IsNullOrWhiteSpace(lastName))
                    return (false, "გვარი არ უნდა იყოს ცარიელი.");

                int age = CalculateAge(birthDate);
                if (age < 18)
                    return (false, "მომხმარებელი უნდა იყოს სრულწლოვანი.");

                if (drivingLicenseExpiration.Date < DateTime.Today)
                    return (false, "მართვის მოწმობა ვადაგასულია — ასეთი მომხმარებელი ვერ დარეგისტრირდება.");

                if (personalNumber.Length != 11)
                    return (false, "პირადი ნომერი უნდა შეიცავდეს 11 სიმბოლოს");

                bool personalNumberExists = _customerRepository.GetAll()
                    .Any(c => c.PersonalNumber == personalNumber);
                if (personalNumberExists)
                    return (false, "ამ პირადი ნომრით მომხმარებელი უკვე არსებობს.");

                bool licenseNumberExists = _customerRepository.GetAll()
                    .Any(c => c.DrivingLicenseNumber == drivingLicenseNumber);
                if (licenseNumberExists)
                    return (false, "ამ მართვის მოწმობის ნომრით მომხმარებელი უკვე არსებობს.");

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PersonalNumber = personalNumber,
                    PhoneNumber = phoneNumber,
                    DrivingLicenseNumber = drivingLicenseNumber,
                    DrivingLicenseExpiration = drivingLicenseExpiration,
                    BirthDate = birthDate
                };

                _customerRepository.Add(customer);
                _customerRepository.Save();

                return (true, "მომხმარებელი წარმატებით დაემატა.");
            }

            // --- Edit ---
            public (bool Success, string Message) UpdateCustomer(
                int id, string firstName, string lastName,
                string phoneNumber, DateTime drivingLicenseExpiration)
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                    return (false, "მომხმარებელი ვერ მოიძებნა.");

                if (string.IsNullOrWhiteSpace(firstName))
                    return (false, "სახელი არ უნდა იყოს ცარიელი.");

                if (string.IsNullOrWhiteSpace(lastName))
                    return (false, "გვარი არ უნდა იყოს ცარიელი.");

                customer.FirstName = firstName;
                customer.LastName = lastName;
                customer.PhoneNumber = phoneNumber;
                customer.DrivingLicenseExpiration = drivingLicenseExpiration;

                _customerRepository.Update(customer);
                _customerRepository.Save();

                return (true, "მომხმარებელი წარმატებით განახლდა.");
            }

            // --- Delete ---
            public (bool Success, string Message) DeleteCustomer(int id)
            {
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                    return (false, "მომხმარებელი ვერ მოიძებნა.");

                bool hasActiveRental = _rentalRepository.GetAll()
                    .Any(r => r.CustomerId == id && r.Status == Enums.RentalStatus.Active);

                if (hasActiveRental)
                    return (false, "მომხმარებლის წაშლა შეუძლებელია — მას აქვს აქტიური გაქირავება.");

                _customerRepository.Delete(id);
                _customerRepository.Save();

                return (true, "მომხმარებელი წარმატებით წაიშალა.");
            }

            // --- search ---
            public Customer SearchByPersonalNumber(string personalNumber)
            {
                return _customerRepository.GetAll()
                    .FirstOrDefault(c => c.PersonalNumber == personalNumber) ?? throw new Exception("Failed To load Customer");
            }

            public Customer SearchByPhoneNumber(string phoneNumber)
            {
                return _customerRepository.GetAll()
                    .FirstOrDefault(c => c.PhoneNumber == phoneNumber) ?? throw new Exception("Failed To load Customer"); ;
            }
            
            // checks Valide Driving Licence
            public bool HasValidDrivingLicense(Customer customer)
            {
                return customer.DrivingLicenseExpiration.Date >= DateTime.Today;
            }


            private int CalculateAge(DateTime birthDate)
            {
                var today = DateTime.Today;
                var age = today.Year - birthDate.Year;
                if (birthDate.Date > today.AddYears(-age)) age--;
                return age;
            }
    }
}

using CarRental_Management.Entities;
using CarRental_Management.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Services
{
    internal class CustomerService
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


        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}

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
    }
}

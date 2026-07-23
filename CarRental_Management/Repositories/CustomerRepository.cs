using CarRental_Management.Data;
using CarRental_Management.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalDbContext _context;

        public CustomerRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Customer entity)
        {
            _context.Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            _context.Customers.Update(entity);
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}

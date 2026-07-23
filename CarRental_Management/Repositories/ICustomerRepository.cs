using CarRental_Management.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
        void Add(Customer entity);
        void Update(Customer entity);
        void Delete(int id);
        void Save();
    }
}

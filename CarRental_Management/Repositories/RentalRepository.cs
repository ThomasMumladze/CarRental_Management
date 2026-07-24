using CarRental_Management.Data;
using CarRental_Management.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalDbContext _context;

        public RentalRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Rental> GetAll()
        {
            return _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .ToList();
        }

        public Rental GetById(int id)
        {
            return _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == id);
        }

        public void Add(Rental entity)
        {
            _context.Rentals.Add(entity);
        }

        public void Update(Rental entity)
        {
            _context.Rentals.Update(entity);
        }

        public void Delete(int id)
        {
            var rental = _context.Rentals.FirstOrDefault(r => r.Id == id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

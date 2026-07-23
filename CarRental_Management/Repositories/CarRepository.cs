using CarRental_Management.Data;
using CarRental_Management.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    internal class CarRepository : ICarRepository
    {
          private readonly CarRentalDbContext _context;

        public CarRepository(CarRentalDbContext context)
        {
            _context = context;
        }

        public List<Car> GetAll()
        {
            return _context.Cars.ToList();
        }

        public Car GetById(int id)
        {
            return _context.Cars.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
        }

        public void Update(Car entity)
        {
            _context.Cars.Update(entity);
        }

        public void Delete(int id)
        {
            var car = GetById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

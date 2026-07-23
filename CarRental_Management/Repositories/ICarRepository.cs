using CarRental_Management.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car GetById(int id);
        void Add(Car entity);
        void Update(Car entity);
        void Delete(int id);
        void Save();
    }
}

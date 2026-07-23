using CarRental_Management.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Repositories
{
    internal interface IRentalRepository
    {
        List<Rental> GetAll();
        Rental GetById(int id);
        void Add(Rental entity);
        void Update(Rental entity);
        void Delete(int id);
        void Save();
    }
}

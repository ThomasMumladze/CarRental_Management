using CarRental_Management.Entities;
using CarRental_Management.Enums;
using CarRental_Management.Repositories;

namespace CarRental_Management.Services
{
    public class StatisticsService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;

        public StatisticsService(
            ICarRepository carRepository,
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository)
        {
            _carRepository = carRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
        }

        public int GetTotalCarsCount() => _carRepository.GetAll().Count;

        public int GetAvailableCarsCount() =>
            _carRepository.GetAll().Count(c => c.IsAvailable);

        public int GetRentedCarsCount() =>
            _carRepository.GetAll().Count(c => !c.IsAvailable);

        public int GetTotalCustomersCount() => _customerRepository.GetAll().Count;

        public int GetActiveRentalsCount() =>
            _rentalRepository.GetAll().Count(r => r.Status == RentalStatus.Active);

        public int GetCompletedRentalsCount() =>
            _rentalRepository.GetAll().Count(r => r.Status == RentalStatus.Completed);

        public decimal GetTotalRevenue() =>
            _rentalRepository.GetAll()
                .Where(r => r.Status == RentalStatus.Completed || r.Status == RentalStatus.Overdue)
                .Sum(r => r.TotalPrice);

        public Car GetMostRentedCar()
        {
            var rentals = _rentalRepository.GetAll();
            if (!rentals.Any()) return null;

            var mostRentedCarId = rentals
                .GroupBy(r => r.CarId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return _carRepository.GetById(mostRentedCarId);
        }

        public Customer GetMostActiveCustomer()
        {
            var rentals = _rentalRepository.GetAll();
            if (!rentals.Any()) return null;

            var mostActiveCustomerId = rentals
                .GroupBy(r => r.CustomerId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            return _customerRepository.GetById(mostActiveCustomerId);
        }
    }
}

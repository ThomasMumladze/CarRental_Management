using CarRental_Management.Entities;
using CarRental_Management.Enums;
using CarRental_Management.Repositories;

namespace CarRental_Management.Services
{
    internal class RentalServices
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly ICustomerRepository _customerRepository;

        public RentalServices(
            IRentalRepository rentalRepository,
            ICarRepository carRepository,
            ICustomerRepository customerRepository)
                {
                    _rentalRepository = rentalRepository;
                    _carRepository = carRepository;
                    _customerRepository = customerRepository;
                }

        public List<Rental> GetAllRentals()
            {
                return _rentalRepository.GetAll()
                    .OrderByDescending(r => r.StartDate)
                    .ToList();
            }

        public List<Rental> GetActiveRentals()
        {
            return _rentalRepository.GetAll()
                .Where(r => r.Status == RentalStatus.Active)
                .OrderBy(r => r.EndDate)
                .ToList();
        }

        public List<Rental> GetCustomerRentalHistory(int customerId)
        {
            return _rentalRepository.GetAll()
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.StartDate)
                .ToList();
        }

        // --- Rental ---
        public (bool Success, string Message, Rental Rental) RentCar(
            int customerId, int carId, DateTime startDate, DateTime endDate)
        {
            var customer = _customerRepository.GetById(customerId);
            if (customer == null)
                return (false, "მომხმარებელი ვერ მოიძებნა.", null);

            var car = _carRepository.GetById(carId);
            if (car == null)
                return (false, "ავტომობილი ვერ მოიძებნა.", null);

            if (!car.IsAvailable)
                return (false, "ავტომობილი ამჟამად არ არის ხელმისაწვდომი.", null);

            if (customer.DrivingLicenseExpiration.Date < DateTime.Today)
                return (false, "მომხმარებლის მართვის მოწმობა ვადაგასულია.", null);

            if (endDate.Date < startDate.Date)
                return (false, "დასრულების თარიღი არ შეიძლება იყოს დაწყების თარიღზე ადრე.", null);

            int days = (endDate.Date - startDate.Date).Days;
            if (days < 1) days = 1; 

            decimal totalPrice = days * car.DailyPrice;

            var rental = new Rental
            {
                CarId = carId,
                CustomerId = customerId,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice,
                Status = RentalStatus.Active
            };

            car.IsAvailable = false;

            _rentalRepository.Add(rental);
            _carRepository.Update(car);
            _rentalRepository.Save(); 

            return (true, $"გაქირავება წარმატებით შესრულდა. სრული ღირებულება: {totalPrice} GEL", rental);
        }

        // --- Return ---
        public (bool Success, string Message) ReturnCar(int rentalId, DateTime actualReturnDate)
        {
            var rental = _rentalRepository.GetById(rentalId);
            if (rental == null)
                return (false, "გაქირავება ვერ მოიძებნა.");

            if (rental.Status != RentalStatus.Active)
                return (false, "ეს გაქირავება უკვე დასრულებულია ან გაუქმებულია.");

            rental.ActualReturnDate = actualReturnDate;

            // bonus func --> Delay control
            if (actualReturnDate.Date > rental.EndDate.Date)
            {
                int lateDays = (actualReturnDate.Date - rental.EndDate.Date).Days;
                decimal lateFee = lateDays * rental.Car.DailyPrice * 0.20m;
                rental.TotalPrice += lateFee;
                rental.Status = RentalStatus.Overdue;
            }
            else
            {
                rental.Status = RentalStatus.Completed;
            }

            rental.Car.IsAvailable = true;

            _rentalRepository.Update(rental);
            _carRepository.Update(rental.Car);
            _rentalRepository.Save();

            return (true, "ავტომობილი წარმატებით დაბრუნდა.");
        }
    }
}

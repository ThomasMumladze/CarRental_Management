using CarRental_Management.Entities;
using CarRental_Management.Enums;
using CarRental_Management.Repositories;


namespace CarRental_Management.Services
{
    public class CarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        public Car GetCarById(int id)
        {
            return _carRepository.GetById(id);
        }

        public List<Car> GetAvailableCars()
        {
            return _carRepository.GetAll()
                .Where(c => c.IsAvailable)
                .ToList();
        }

        // --- add new car ---
        public (bool Success, string Message) AddCar(
            string brand, string model, int year, string licensePlate,
            decimal dailyPrice, CarCategory category,
            TransmissionType transmission, FuelType fuelType)
        {
            // validation
            if (string.IsNullOrWhiteSpace(brand))
                return (false, "ბრენდი არ უნდა იყოს ცარიელი.");

            if (string.IsNullOrWhiteSpace(model))
                return (false, "მოდელი არ უნდა იყოს ცარიელი.");

            if (year < 1950 || year > DateTime.Now.Year + 1)
                return (false, "ავტომობილის წელი არარეალურია.");

            if (dailyPrice <= 0)
                return (false, "დღიური ფასი უნდა იყოს ნულზე მეტი.");

            if (string.IsNullOrWhiteSpace(licensePlate))
                return (false, "სახელმწიფო ნომერი არ უნდა იყოს ცარიელი.");

            bool plateExists = _carRepository.GetAll()
                .Any(c => c.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
            if (plateExists)
                return (false, "ამ სახელმწიფო ნომრით ავტომობილი უკვე არსებობს.");

            var car = new Car
            {
                Brand = brand,
                Model = model,
                Year = year,
                LicensePlate = licensePlate,
                DailyPrice = dailyPrice,
                Category = category,
                Transmission = transmission,
                FuelType = fuelType,
                IsAvailable = true 
            };

            _carRepository.Add(car);
            _carRepository.Save();

            return (true, "ავტომობილი წარმატებით დაემატა.");
        }

        // --- editing ---
        public (bool Success, string Message) UpdateCar(
            int id, string brand, string model, int year,
            decimal dailyPrice, CarCategory category,
            TransmissionType transmission, FuelType fuelType)
        {
            var car = _carRepository.GetById(id);
            if (car == null)
                return (false, "ავტომობილი ვერ მოიძებნა.");

            if (!car.IsAvailable)
                return (false, "გაქირავებული ავტომობილის რედაქტირება არ არის შესაძლებელი.");

            if (string.IsNullOrWhiteSpace(brand))
                return (false, "ბრენდი არ უნდა იყოს ცარიელი.");

            if (string.IsNullOrWhiteSpace(model))
                return (false, "მოდელი არ უნდა იყოს ცარიელი.");

            if (year < 1950 || year > DateTime.Now.Year + 1)
                return (false, "ავტომობილის წელი არარეალურია.");

            if (dailyPrice <= 0)
                return (false, "დღიური ფასი უნდა იყოს ნულზე მეტი.");

            car.Brand = brand;
            car.Model = model;
            car.Year = year;
            car.DailyPrice = dailyPrice;
            car.Category = category;
            car.Transmission = transmission;
            car.FuelType = fuelType;

            _carRepository.Update(car);
            _carRepository.Save();

            return (true, "ავტომობილი წარმატებით განახლდა.");
        }

        // --- delete ---
        public (bool Success, string Message) DeleteCar(int id)
        {
            var car = _carRepository.GetById(id);
            if (car == null)
                return (false, "ავტომობილი ვერ მოიძებნა.");

            if (!car.IsAvailable)
                return (false, "გაქირავებული ავტომობილის წაშლა არ არის შესაძლებელი.");

            _carRepository.Delete(id);
            _carRepository.Save();

            return (true, "ავტომობილი წარმატებით წაიშალა.");
        }

        // --- Search/Filter ---
        public List<Car> SearchByBrand(string brand)
        {
            return _carRepository.GetAll()
                .Where(c => c.Brand.Contains(brand, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Car> SearchByModel(string model)
        {
            return _carRepository.GetAll()
                .Where(c => c.Model.Contains(model, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Car> FilterByCategory(CarCategory category)
        {
            return _carRepository.GetAll()
                .Where(c => c.Category == category)
                .ToList();
        }

        public List<Car> FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _carRepository.GetAll()
                .Where(c => c.DailyPrice >= minPrice && c.DailyPrice <= maxPrice)
                .OrderBy(c => c.DailyPrice)
                .ToList();
        }
    }
}

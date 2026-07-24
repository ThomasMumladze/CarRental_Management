using CarRental_Management.Enums;
using CarRental_Management.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.helper
{
    public class CarMenu
    {
        private readonly CarService _carService;

        public CarMenu(CarService carService)
        {
            _carService = carService;
        }

        public void Show()
        {
            bool inCarMenu = true;

            while (inCarMenu)
            {
                Console.WriteLine("\n==== ავტომობილების მართვა ====");
                foreach (CarMenuOption option in Enum.GetValues(typeof(CarMenuOption)))
                {
                    Console.WriteLine($"{(int)option}. {option.GetDescription()}");
                }
                Console.Write("\nაირჩიეთ პუნქტი: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(CarMenuOption), choice))
                {
                    Console.WriteLine("არასწორი მონაცემი! სცადეთ ხელახლა.");
                    continue;
                }

                switch ((CarMenuOption)choice)
                {
                    case CarMenuOption.ViewAll:
                        ViewAllCars();
                        break;
                    case CarMenuOption.Add:
                        AddCar();
                        break;
                    case CarMenuOption.ViewDetails:
                        ViewCarDetails();
                        break;
                    case CarMenuOption.Edit:
                        EditCar();
                        break;
                    case CarMenuOption.Delete:
                        DeleteCar();
                        break;
                    case CarMenuOption.ViewAvailable:
                        ViewAvailableCars();
                        break;
                    case CarMenuOption.Back:
                        inCarMenu = false;
                        break;
                }
            }
        }

        private void ViewAllCars()
        {
            var cars = _carService.GetAllCars();
            PrintCarTable(cars);
        }

        private void ViewAvailableCars()
        {
            var cars = _carService.GetAvailableCars();
            PrintCarTable(cars);
        }

        private void PrintCarTable(System.Collections.Generic.List<Entities.Car> cars)
        {
            if (!cars.Any())
            {
                Console.WriteLine("ავტომობილები არ მოიძებნა.");
                return;
            }

            Console.WriteLine($"\n{"ID",-4}| {"Brand",-10}| {"Model",-10}| {"Year",-6}| {"Plate",-10}| {"Price",-8}| Available");
            Console.WriteLine(new string('-', 70));

            foreach (var car in cars)
            {
                Console.WriteLine(
                    $"{car.Id,-4}| {car.Brand,-10}| {car.Model,-10}| {car.Year,-6}| {car.LicensePlate,-10}| {car.DailyPrice,-8:0.00}| {(car.IsAvailable ? "Yes" : "No")}");
            }
        }

        private void AddCar()
        {
            try
            {
                Console.Write("ბრენდი: ");
                string brand = Console.ReadLine();

                Console.Write("მოდელი: ");
                string model = Console.ReadLine();

                Console.Write("გამოშვების წელი: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("არასწორი წელი.");
                    return;
                }

                Console.Write("სახელმწიფო ნომერი: ");
                string plate = Console.ReadLine();

                Console.Write("დღიური ფასი: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("არასწორი ფასი.");
                    return;
                }

                var category = ReadEnum<CarCategory>("კატეგორია (Economy, Sedan, SUV, Sports, Luxury, Van): ");
                var fuelType = ReadEnum<FuelType>("საწვავის ტიპი (Petrol, Diesel, Hybrid, Electric): ");
                var transmission = ReadEnum<TransmissionType>("გადაცემათა კოლოფი (Manual, Automatic): ");

                var (success, message) = _carService.AddCar(
                    brand, model, year, plate, price, category, transmission, fuelType);

                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
            }
        }

        private void ViewCarDetails()
        {
            Console.Write("ავტომობილის ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var car = _carService.GetCarById(id);
            if (car == null)
            {
                Console.WriteLine("ავტომობილი ვერ მოიძებნა.");
                return;
            }

            Console.WriteLine($"\nID: {car.Id}");
            Console.WriteLine($"ბრენდი: {car.Brand}");
            Console.WriteLine($"მოდელი: {car.Model}");
            Console.WriteLine($"წელი: {car.Year}");
            Console.WriteLine($"ნომერი: {car.LicensePlate}");
            Console.WriteLine($"ფასი: {car.DailyPrice} GEL/დღეში");
            Console.WriteLine($"კატეგორია: {car.Category}");
            Console.WriteLine($"ტრანსმისია: {car.Transmission}");
            Console.WriteLine($"საწვავი: {car.FuelType}");
            Console.WriteLine($"ხელმისაწვდომია: {(car.IsAvailable ? "დიახ" : "არა")}");
        }

        private void EditCar()
        {
            try
            {
                Console.Write("ავტომობილის ID (რედაქტირებისთვის): ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                var existing = _carService.GetCarById(id);
                if (existing == null)
                {
                    Console.WriteLine("ავტომობილი ვერ მოიძებნა.");
                    return;
                }

                Console.WriteLine($"მიმდინარე მონაცემები: {existing.Brand} {existing.Model}, {existing.Year}, {existing.DailyPrice} GEL");

                Console.Write($"ახალი ბრენდი [{existing.Brand}]: ");
                string brand = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(brand)) brand = existing.Brand;

                Console.Write($"ახალი მოდელი [{existing.Model}]: ");
                string model = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(model)) model = existing.Model;

                Console.Write($"ახალი წელი [{existing.Year}]: ");
                string yearInput = Console.ReadLine();
                int year = string.IsNullOrWhiteSpace(yearInput) ? existing.Year : int.Parse(yearInput);

                Console.Write($"ახალი ფასი [{existing.DailyPrice}]: ");
                string priceInput = Console.ReadLine();
                decimal price = string.IsNullOrWhiteSpace(priceInput) ? existing.DailyPrice : decimal.Parse(priceInput);

                var category = ReadEnum<CarCategory>($"ახალი კატეგორია [{existing.Category}]: ", existing.Category);
                var transmission = ReadEnum<TransmissionType>($"ახალი ტრანსმისია [{existing.Transmission}]: ", existing.Transmission);
                var fuelType = ReadEnum<FuelType>($"ახალი საწვავი [{existing.FuelType}]: ", existing.FuelType);

                var (success, message) = _carService.UpdateCar(
                    id, brand, model, year, price, category, transmission, fuelType);

                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
            }
        }

        private void DeleteCar()
        {
            Console.Write("ავტომობილის ID (წასაშლელად): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var car = _carService.GetCarById(id);
            if (car == null)
            {
                Console.WriteLine("ავტომობილი ვერ მოიძებნა.");
                return;
            }

            Console.Write($"ნამდვილად გსურთ ავტომობილის წაშლა ({car.Brand} {car.Model})? Y/N: ");
            string confirm = Console.ReadLine();

            if (confirm?.Trim().ToUpper() != "Y")
            {
                Console.WriteLine("წაშლა გაუქმდა.");
                return;
            }

            var (success, message) = _carService.DeleteCar(id);
            Console.WriteLine(message);
        }

        
        private T ReadEnum<T>(string prompt, T? defaultValue = null) where T : struct, Enum
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) && defaultValue.HasValue)
                    return defaultValue.Value;

                if (Enum.TryParse<T>(input, true, out T result) && Enum.IsDefined(typeof(T), result))
                    return result;

                Console.WriteLine("არასწორი მნიშვნელობა, სცადეთ ხელახლა.");
            }
        }
    }
}

using CarRental_Management.Services;


namespace CarRental_Management.helper
{
    public class RentalMenu
    {
        private readonly RentalServices _rentalService;
        private readonly CarService _carService;
        private readonly CustomerService _customerService;

        public RentalMenu(RentalServices rentalService, CarService carService, CustomerService customerService)
        {
            _rentalService = rentalService;
            _carService = carService;
            _customerService = customerService;
        }

        public void RentCarFlow()
        {
            try
            {
                Console.Write("მომხმარებლის ID: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                Console.Write("ავტომობილის ID: ");
                if (!int.TryParse(Console.ReadLine(), out int carId))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                Console.Write("დაწყების თარიღი (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("არასწორი თარიღი.");
                    return;
                }

                Console.Write("დასრულების თარიღი (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("არასწორი თარიღი.");
                    return;
                }

                var car = _carService.GetCarById(carId);
                if (car == null)
                {
                    Console.WriteLine("ავტომობილი ვერ მოიძებნა.");
                    return;
                }

                int days = Math.Max(1, (endDate.Date - startDate.Date).Days);
                decimal estimatedPrice = days * car.DailyPrice;

                Console.WriteLine($"\nგაქირავების დღეები: {days}");
                Console.WriteLine($"დღიური ფასი: {car.DailyPrice} GEL");
                Console.WriteLine($"სრული ღირებულება: {estimatedPrice} GEL");
                Console.Write("\nდაადასტურეთ გაქირავება: Y/N: ");

                string confirm = Console.ReadLine();
                if (confirm?.Trim().ToUpper() != "Y")
                {
                    Console.WriteLine("გაქირავება გაუქმდა.");
                    return;
                }

                var (success, message, rental) = _rentalService.RentCar(customerId, carId, startDate, endDate);
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
            }
        }

        public void ReturnCarFlow()
        {
            try
            {
                Console.Write("Rental ID: ");
                if (!int.TryParse(Console.ReadLine(), out int rentalId))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                Console.Write("დაბრუნების თარიღი (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
                {
                    Console.WriteLine("არასწორი თარიღი.");
                    return;
                }

                var (success, message) = _rentalService.ReturnCar(rentalId, returnDate);
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
            }
        }

        public void ViewActiveRentals()
        {
            var rentals = _rentalService.GetActiveRentals();
            if (!rentals.Any())
            {
                Console.WriteLine("აქტიური გაქირავებები არ მოიძებნა.");
                return;
            }

            foreach (var r in rentals)
            {
                Console.WriteLine($"\nRental ID: {r.Id}");
                Console.WriteLine($"Customer: {r.Customer.FirstName} {r.Customer.LastName}");
                Console.WriteLine($"Car: {r.Car.Brand} {r.Car.Model}");
                Console.WriteLine($"Start Date: {r.StartDate:yyyy-MM-dd}");
                Console.WriteLine($"End Date: {r.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Total Price: {r.TotalPrice} GEL");
                Console.WriteLine($"Status: {r.Status}");
            }
        }

        public void ViewRentalHistory()
        {
            var rentals = _rentalService.GetAllRentals();
            if (!rentals.Any())
            {
                Console.WriteLine("გაქირავებების ისტორია ცარიელია.");
                return;
            }

            foreach (var r in rentals)
            {
                Console.WriteLine($"\nRental ID: {r.Id}");
                Console.WriteLine($"მომხმარებელი: {r.Customer.FirstName} {r.Customer.LastName}");
                Console.WriteLine($"ავტომობილი: {r.Car.Brand} {r.Car.Model}");
                Console.WriteLine($"დაწყება: {r.StartDate:yyyy-MM-dd} | დასრულება: {r.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"ფასი: {r.TotalPrice} GEL | სტატუსი: {r.Status}");
            }
        }
    }
}

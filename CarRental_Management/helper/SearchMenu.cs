using CarRental_Management.Enums;
using CarRental_Management.Services;

namespace CarRental_Management.helper
{
    public class SearchMenu
    {
        private readonly CarService _carService;
        private readonly CustomerService _customerService;

        public SearchMenu(CarService carService, CustomerService customerService)
        {
            _carService = carService;
            _customerService = customerService;
        }

        public void Show()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.WriteLine("\n==== ძებნა და ფილტრაცია ====");
                foreach (SearchMenuOption option in Enum.GetValues(typeof(SearchMenuOption)))
                {
                    Console.WriteLine($"{(int)option}. {option.GetDescription()}");
                }
                Console.Write("\nაირჩიეთ პუნქტი: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(SearchMenuOption), choice))
                {
                    Console.WriteLine("არასწორი მონაცემი!");
                    continue;
                }

                try
                {
                    switch ((SearchMenuOption)choice)
                    {
                        case SearchMenuOption.SearchByBrand:
                            Console.Write("შეიყვანეთ ბრენდი: ");
                            PrintCars(_carService.SearchByBrand(Console.ReadLine()));
                            break;

                        case SearchMenuOption.SearchByModel:
                            Console.Write("შეიყვანეთ მოდელი: ");
                            PrintCars(_carService.SearchByModel(Console.ReadLine()));
                            break;

                        case SearchMenuOption.FilterByCategory:
                            Console.Write("კატეგორია (Economy, Sedan, SUV, Sports, Luxury, Van): ");
                            if (Enum.TryParse(Console.ReadLine(), true, out CarCategory category))
                                PrintCars(_carService.FilterByCategory(category));
                            else
                                Console.WriteLine("არასწორი კატეგორია.");
                            break;

                        case SearchMenuOption.FilterByPrice:
                            Console.Write("მინიმალური ფასი: ");
                            decimal.TryParse(Console.ReadLine(), out decimal min);
                            Console.Write("მაქსიმალური ფასი: ");
                            decimal.TryParse(Console.ReadLine(), out decimal max);
                            PrintCars(_carService.FilterByPriceRange(min, max));
                            break;

                        case SearchMenuOption.AvailableCars:
                            PrintCars(_carService.GetAvailableCars());
                            break;

                        case SearchMenuOption.SearchCustomerByPersonalNumber:
                            Console.Write("პირადი ნომერი: ");
                            var c1 = _customerService.SearchByPersonalNumber(Console.ReadLine());
                            PrintCustomer(c1);
                            break;

                        case SearchMenuOption.SearchCustomerByPhone:
                            Console.Write("ტელეფონის ნომერი: ");
                            var c2 = _customerService.SearchByPhoneNumber(Console.ReadLine());
                            PrintCustomer(c2);
                            break;

                        case SearchMenuOption.Back:
                            inMenu = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
                }
            }
        }

        private void PrintCars(System.Collections.Generic.List<Entities.Car> cars)
        {
            if (!cars.Any())
            {
                Console.WriteLine("ავტომობილები არ მოიძებნა.");
                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Id}. {car.Brand} {car.Model} ({car.Year}) - {car.DailyPrice} GEL - {(car.IsAvailable ? "ხელმისაწვდომია" : "დაკავებულია")}");
            }
        }

        private void PrintCustomer(Entities.Customer customer)
        {
            if (customer == null)
            {
                Console.WriteLine("მომხმარებელი ვერ მოიძებნა.");
                return;
            }

            Console.WriteLine($"{customer.Id}. {customer.FirstName} {customer.LastName} - {customer.PhoneNumber}");
        }
    }
}

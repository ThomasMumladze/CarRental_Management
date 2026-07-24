using CarRental_Management.Data;
using CarRental_Management.Enums;
using CarRental_Management.helper;
using CarRental_Management.Menu;
using CarRental_Management.Repositories;
using CarRental_Management.Services;
using System.Text;

namespace CarRental_Management
{
    class Program
    {
        private CarRentalDbContext _context;

        private CarMenu _carMenu;
        private CustomerMenu _customerMenu;
        private RentalMenu _rentalMenu;
        private SearchMenu _searchMenu;
        private StatisticsMenu _statisticsMenu;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var app = new Program();
            app.Initialize();
            app.Run();
        }

        private void Initialize()
        {
            _context = new CarRentalDbContext();

            var carRepository = new CarRepository(_context);
            var customerRepository = new CustomerRepository(_context);
            var rentalRepository = new RentalRepository(_context);

            var carService = new CarService(carRepository);
            var customerService = new CustomerService(customerRepository, rentalRepository);
            var rentalService = new RentalServices(rentalRepository, carRepository, customerRepository);
            var statisticsService = new StatisticsService(carRepository, customerRepository, rentalRepository);

            _carMenu = new CarMenu(carService);
            _customerMenu = new CustomerMenu(customerService, rentalService);
            _rentalMenu = new RentalMenu(rentalService, carService, customerService);
            _searchMenu = new SearchMenu(carService, customerService);
            _statisticsMenu = new StatisticsMenu(statisticsService);
        }

        private void Run()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("       CAR RENTAL MANAGEMENT");
                Console.WriteLine("====================================\n");

                foreach (MenuOption option in Enum.GetValues(typeof(MenuOption)))
                {
                    Console.WriteLine($"{(int)option}. {option.GetDescription()}");
                }
                Console.Write("\nაირჩიეთ მოქმედება: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(MenuOption), choice))
                {
                    Console.WriteLine("არასწორი მონაცემი! სცადეთ ხელახლა.");
                    continue;
                }

                try
                {
                    switch ((MenuOption)choice)
                    {
                        case MenuOption.ManageCars:
                            _carMenu.Show();
                            break;

                        case MenuOption.ManageCustomers:
                            _customerMenu.Show();
                            break;

                        case MenuOption.RentCar:
                            _rentalMenu.RentCarFlow();
                            break;

                        case MenuOption.ReturnCar:
                            _rentalMenu.ReturnCarFlow();
                            break;

                        case MenuOption.ViewActiveRentals:
                            _rentalMenu.ViewActiveRentals();
                            break;

                        case MenuOption.RentalHistory:
                            _rentalMenu.ViewRentalHistory();
                            break;

                        case MenuOption.SearchAndFilter:
                            _searchMenu.Show();
                            break;

                        case MenuOption.Statistics:
                            _statisticsMenu.Show();
                            break;

                        case MenuOption.ExitProgram:
                            isRunning = false;
                            Console.WriteLine("პროგრამა დასრულდა.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ოპერაცია ვერ შესრულდა: {ex.Message}");
                }
            }

            _context.Dispose();
        }
    }
}


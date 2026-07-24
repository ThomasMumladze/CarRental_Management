using CarRental_Management.Data;
using CarRental_Management.Enums;
using CarRental_Management.helper;
using CarRental_Management.Repositories;
using CarRental_Management.Services;
using System.Text;

namespace CarRental_Management
{
    class Program
    {
        private CarRentalDbContext _context;
        private CarService _carService;
        private CustomerService _customerService;
        private RentalServices _rentalService;

        private CarMenu _carMenu;

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

            _carService = new CarService(carRepository);
            _customerService = new CustomerService(customerRepository, rentalRepository);
            _rentalService = new RentalServices(rentalRepository, carRepository, customerRepository);

            _carMenu = new CarMenu(_carService);
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
                            Console.WriteLine("→ მომხმარებლების მართვა (მალე დაემატება)");
                            break;

                        case MenuOption.RentCar:
                            Console.WriteLine("→ ავტომობილის გაქირავება (მალე დაემატება)");
                            break;

                        case MenuOption.ReturnCar:
                            Console.WriteLine("→ ავტომობილის დაბრუნება (მალე დაემატება)");
                            break;

                        case MenuOption.ViewActiveRentals:
                            Console.WriteLine("→ აქტიური გაქირავებები (მალე დაემატება)");
                            break;

                        case MenuOption.RentalHistory:
                            Console.WriteLine("→ გაქირავებების ისტორია (მალე დაემატება)");
                            break;

                        case MenuOption.SearchAndFilter:
                            Console.WriteLine("→ ძებნა და ფილტრაცია (მალე დაემატება)");
                            break;

                        case MenuOption.Statistics:
                            Console.WriteLine("→ სტატისტიკა (მალე დაემატება)");
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

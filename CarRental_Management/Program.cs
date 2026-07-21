using CarRental_Management.Enums;
using System.Text;

namespace CarRental_Management
{
    class Program
    {
        public static MenuOption _menuOptions;
        public static MenuOption _menuOption;
        public static void Main(string[] args)
        {
            // Helps Read Georgian Alphabet
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var app = new Program();

            app.Run();
        }

        private void Run()
        {
            bool isRuning = true;

            while (isRuning)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("       CAR RENTAL MANAGEMENT");
                Console.WriteLine("====================================\n");

                ShowMenu();

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choise) || !Enum.IsDefined(typeof(MenuOption), choise))
                {
                    Console.WriteLine("\nარასწორი მონაცემი! დააჭირეთ Enter-ს გასაგრძელებლად...");
                    Console.ReadLine();
                    continue;
                }

                _menuOption = (MenuOption)choise;
                isRuning = HandleSelection();
            }
        }

        //showing Menu Options
        private void ShowMenu()
        {
            foreach (MenuOption menuOption in Enum.GetValues(typeof(MenuOption))) {
                Console.WriteLine($"{(int)menuOption}. {menuOption.GetDescription()}");
            }
        }

        private bool HandleSelection()
        {
            switch (_menuOption)
            {
                case MenuOption.ManageCars:
                    Console.WriteLine("\n→ ავტომობილების მართვის მენიუ");
                    break;

                case MenuOption.ManageCustomers:
                    Console.WriteLine("\n→ მომხმარებლების მართვის მენიუ");
                    break;

                case MenuOption.RentCar:
                    Console.WriteLine("\n→ ავტომობილის გაქირავება");
                    break;

                case MenuOption.ReturnCar:
                    Console.WriteLine("\n→ ავტომობილის დაბრუნება");
                    break;

                case MenuOption.ViewActiveRentals:
                    Console.WriteLine("\n→ აქტიური გაქირავებები");
                    break;

                case MenuOption.RentalHistory:
                    Console.WriteLine("\n→ გაქირავებების ისტორია");
                    break;

                case MenuOption.SearchAndFilter:
                    Console.WriteLine("\n→ ძებნა და ფილტრაცია");
                    break;

                case MenuOption.Statistics:
                    Console.WriteLine("\n→ სტატისტიკა");
                    break;

                case MenuOption.ExitProgram:
                    Console.WriteLine("\nპროგრამა დასრულდა.");
                    return false;
            }

            Console.WriteLine("\nდააჭირეთ Enter-ს გასაგრძელებლად...");
            Console.ReadLine();
            return true;
        }


    }
}

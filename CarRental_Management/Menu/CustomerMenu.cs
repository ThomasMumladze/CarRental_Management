using CarRental_Management.Enums;
using CarRental_Management.helper;
using CarRental_Management.Services;
using CarRental_Management.helper;

namespace CarRental_Management.Menu
{
    public class CustomerMenu
    {
        private readonly CustomerService _customerService;
        private readonly RentalServices _rentalService;

        public CustomerMenu(CustomerService customerService, RentalServices rentalService)
        {
            _customerService = customerService;
            _rentalService = rentalService;
        }

        public void Show()
        {
            bool inMenu = true;

            while (inMenu)
            {
                Console.WriteLine("\n==== მომხმარებლების მართვა ====");
                foreach (CustomerMenuOption option in Enum.GetValues(typeof(CustomerMenuOption)))
                {
                    Console.WriteLine($"{(int)option}. {option.GetDescription()}");
                }
                Console.Write("\nაირჩიეთ პუნქტი: ");

                string input = Console.ReadLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(CustomerMenuOption), choice))
                {
                    Console.WriteLine("არასწორი მონაცემი! სცადეთ ხელახლა.");
                    continue;
                }

                try
                {
                    switch ((CustomerMenuOption)choice)
                    {
                        case CustomerMenuOption.ViewAll:
                            ViewAllCustomers();
                            break;
                        case CustomerMenuOption.Add:
                            AddCustomer();
                            break;
                        case CustomerMenuOption.ViewDetails:
                            ViewCustomerDetails();
                            break;
                        case CustomerMenuOption.Edit:
                            EditCustomer();
                            break;
                        case CustomerMenuOption.Delete:
                            DeleteCustomer();
                            break;
                        case CustomerMenuOption.RentalHistory:
                            ViewCustomerRentalHistory();
                            break;
                        case CustomerMenuOption.Back:
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

        private void ViewAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            if (!customers.Any())
            {
                Console.WriteLine("მომხმარებლები არ მოიძებნა.");
                return;
            }

            Console.WriteLine($"\n{"ID",-4}| {"სახელი",-12}| {"გვარი",-12}| {"ტელეფონი",-14}| მართვის მოწმობა");
            Console.WriteLine(new string('-', 70));

            foreach (var c in customers)
            {
                string licenseStatus = c.DrivingLicenseExpiration.Date < DateTime.Today ? "ვადაგასულია" : "მოქმედია";
                Console.WriteLine($"{c.Id,-4}| {c.FirstName,-12}| {c.LastName,-12}| {c.PhoneNumber,-14}| {licenseStatus}");
            }
        }

        private void AddCustomer()
        {
            Console.WriteLine("(ნებისმიერ ეტაპზე -1 შეიყვანეთ გასაუქმებლად)\n");

            string firstName = ReadNonEmptyString.ReadEmptyString("სახელი: ");
            if (firstName == null) return;

            string lastName = ReadNonEmptyString.ReadEmptyString("გვარი: ");
            if (lastName == null) return;

            string phone = ReadNonEmptyString.ReadEmptyString("ტელეფონის ნომერი: ");
            if (phone == null) return;

            string licenseNumber = ReadNonEmptyString.ReadEmptyString("მართვის მოწმობის ნომერი: ");
            if (licenseNumber == null) return;

            string personalNumber = PersonalNumber.ReadPersonalNumber("პირადი ნომერი (11 ციფრი): ");
            if (personalNumber == null) return;


            DateTime? licenseExpiration = DateReader.ReadDate("მართვის მოწმობის მოქმედების ვადა (yyyy-MM-dd): ");
            if (licenseExpiration == null) return;

            DateTime? birthDate = DateReader.ReadDate("დაბადების თარიღი (yyyy-MM-dd): ");
            if (birthDate == null) return;

            var (success, message) = _customerService.AddCustomer(
                firstName, lastName, personalNumber, phone, licenseNumber,
                licenseExpiration.Value, birthDate.Value);

            Console.WriteLine(message);

            if (!success)
            {
                Console.Write("გსურთ ხელახლა ცდა? Y/N: ");
                if (Console.ReadLine()?.Trim().ToUpper() == "Y")
                {
                    AddCustomer();
                }
                //}
            }
        }

        private void ViewCustomerDetails()
        {
            Console.Write("მომხმარებლის ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                Console.WriteLine("მომხმარებელი ვერ მოიძებნა.");
                return;
            }

            Console.WriteLine($"\nID: {customer.Id}");
            Console.WriteLine($"სახელი: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"პირადი ნომერი: {customer.PersonalNumber}");
            Console.WriteLine($"ტელეფონი: {customer.PhoneNumber}");
            Console.WriteLine($"მართვის მოწმობა: {customer.DrivingLicenseNumber}");
            Console.WriteLine($"მოწმობის ვადა: {customer.DrivingLicenseExpiration:yyyy-MM-dd} " +
                $"({(customer.DrivingLicenseExpiration.Date < DateTime.Today ? "ვადაგასულია" : "მოქმედია")})");
            Console.WriteLine($"დაბადების თარიღი: {customer.BirthDate:yyyy-MM-dd}");
        }

        private void EditCustomer()
        {
            Console.Write("მომხმარებლის ID (რედაქტირებისთვის): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var existing = _customerService.GetCustomerById(id);
            if (existing == null)
            {
                Console.WriteLine("მომხმარებელი ვერ მოიძებნა.");
                return;
            }

            Console.Write($"ახალი სახელი [{existing.FirstName}]: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName)) firstName = existing.FirstName;

            Console.Write($"ახალი გვარი [{existing.LastName}]: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName)) lastName = existing.LastName;

            Console.Write($"ახალი ტელეფონი [{existing.PhoneNumber}]: ");
            string phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(phone)) phone = existing.PhoneNumber;

            Console.Write($"ახალი მოწმობის ვადა [{existing.DrivingLicenseExpiration:yyyy-MM-dd}]: ");
            string expInput = Console.ReadLine();
            DateTime expiration = string.IsNullOrWhiteSpace(expInput)
                ? existing.DrivingLicenseExpiration
                : DateTime.Parse(expInput);

            var (success, message) = _customerService.UpdateCustomer(id, firstName, lastName, phone, expiration);
            Console.WriteLine(message);
        }

        private void DeleteCustomer()
        {
            Console.Write("მომხმარებლის ID (წასაშლელად): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                Console.WriteLine("მომხმარებელი ვერ მოიძებნა.");
                return;
            }

            Console.Write($"ნამდვილად გსურთ მომხმარებლის წაშლა ({customer.FirstName} {customer.LastName})? Y/N: ");
            string confirm = Console.ReadLine();

            if (confirm?.Trim().ToUpper() != "Y")
            {
                Console.WriteLine("წაშლა გაუქმდა.");
                return;
            }

            var (success, message) = _customerService.DeleteCustomer(id);
            Console.WriteLine(message);
        }

        private void ViewCustomerRentalHistory()
        {
            Console.Write("მომხმარებლის ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("არასწორი ID.");
                return;
            }

            var rentals = _rentalService.GetCustomerRentalHistory(id);
            if (!rentals.Any())
            {
                Console.WriteLine("გაქირავებების ისტორია ცარიელია.");
                return;
            }

            foreach (var r in rentals)
            {
                Console.WriteLine($"\nRental ID: {r.Id}");
                Console.WriteLine($"ავტომობილი: {r.Car.Brand} {r.Car.Model}");
                Console.WriteLine($"დაწყება: {r.StartDate:yyyy-MM-dd} | დასრულება: {r.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"ფასი: {r.TotalPrice} GEL | სტატუსი: {r.Status}");
            }
        }
    }
}

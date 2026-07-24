using CarRental_Management.Services;

namespace CarRental_Management.helper
{
    public class StatisticsMenu
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsMenu(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public void Show()
        {
            Console.WriteLine("\n==== სტატისტიკა ====");
            Console.WriteLine($"1. ავტომობილების სრული რაოდენობა: {_statisticsService.GetTotalCarsCount()}");
            Console.WriteLine($"2. ხელმისაწვდომი ავტომობილები: {_statisticsService.GetAvailableCarsCount()}");
            Console.WriteLine($"3. გაქირავებული ავტომობილები: {_statisticsService.GetRentedCarsCount()}");
            Console.WriteLine($"4. მომხმარებლების რაოდენობა: {_statisticsService.GetTotalCustomersCount()}");
            Console.WriteLine($"5. აქტიური გაქირავებები: {_statisticsService.GetActiveRentalsCount()}");
            Console.WriteLine($"6. დასრულებული გაქირავებები: {_statisticsService.GetCompletedRentalsCount()}");
            Console.WriteLine($"7. მიღებული სრული შემოსავალი: {_statisticsService.GetTotalRevenue()} GEL");

            var mostRentedCar = _statisticsService.GetMostRentedCar();
            Console.WriteLine($"8. ყველაზე ხშირად გაქირავებული ავტომობილი: " +
                (mostRentedCar != null ? $"{mostRentedCar.Brand} {mostRentedCar.Model}" : "მონაცემი არ არის"));

            var mostActiveCustomer = _statisticsService.GetMostActiveCustomer();
            Console.WriteLine($"9. ყველაზე აქტიური მომხმარებელი: " +
                (mostActiveCustomer != null ? $"{mostActiveCustomer.FirstName} {mostActiveCustomer.LastName}" : "მონაცემი არ არის"));
        }
    }
}

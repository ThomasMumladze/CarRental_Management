using CarRental_Management.Enums;
using System;
using System.Text;

namespace CarRental_Management
{
    class Program
    {
        public static MenuOption _menuOptions;
        public static void Main(string[] args)
        {   
            // Helps Read Georgian Alphabet
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("====================================\r\n       CAR RENTAL MANAGEMENT\r\n====================================\r\n");
               
            string[] MenuOptions = 
                {
                    "1. ავტომობილების მართვა",
                    "2. მომხმარებლების მართვა",
                    "3. ავტომობილის გაქირავება",
                    "4. ავტომობილის დაბრუნება",
                    "5. აქტიური გაქირავებების ნახვა",
                    "6. გაქირავებების ისტორია",
                    "7. ძებნა და ფილტრაცია",
                    "8. სტატისტიკა",
                    "0. პროგრამიდან გამოსვლა"
                };

                foreach (var item in MenuOptions)
                {
                    Console.WriteLine(item);
                }           
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.helper
{
    public static  class PersonalNumber
    {
        public static string ReadPersonalNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input?.Trim() == "-1")
                    return null; // გაუქმება

                input = input?.Trim();

                if (string.IsNullOrEmpty(input) || input.Length != 11 || !input.All(char.IsDigit))
                {
                    Console.WriteLine("პირადი ნომერი უნდა შედგებოდეს ზუსტად 11 ციფრისგან. სცადეთ ხელახლა (ან -1 გასაუქმებლად).");
                    continue;
                }

                return input;
            }
        }
    }
}

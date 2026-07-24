using System;

namespace CarRental_Management.helper
{
    public  static class ReadNonEmptyString
    {
        public static string ReadEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input?.Trim() == "-1")
                    return null; // გაუქმება

                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("ველი არ შეიძლება იყოს ცარიელი. სცადეთ ხელახლა (ან -1 გასაუქმებლად).");
            }
        }
    }
}

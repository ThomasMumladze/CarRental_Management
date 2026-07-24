using System;

namespace CarRental_Management.helper
{
    public static class DateReader
    {
        public static  DateTime? ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input?.Trim() == "-1")
                    return null; // გაუქმება

                if (DateTime.TryParse(input, out DateTime result))
                    return result;

                Console.WriteLine("არასწორი თარიღის ფორმატი. მაგალითი: 2026-07-24 (ან -1 გასაუქმებლად).");
            }
        }
    }
}

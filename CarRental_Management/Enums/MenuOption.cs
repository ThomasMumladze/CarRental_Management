using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CarRental_Management.Enums
{
    public enum MenuOption
    {
        [Description("პროგრამიდან გამოსვლა")]
        ExitProgram = 0,

        [Description("ავტომობილების მართვა")]
        ManageCars = 1,

        [Description("მომხმარებლების მართვა")]
        ManageCustomers = 2,

        [Description("ავტომობილის გაქირავება")]
        RentCar = 3,

        [Description("ავტომობილის დაბრუნება")]
        ReturnCar = 4,

        [Description("აქტიური გაქირავებების ნახვა")]
        ViewActiveRentals = 5,

        [Description("გაქირავებების ისტორია")]
        RentalHistory = 6,

        [Description("ძებნა და ფილტრაცია")]
        SearchAndFilter = 7,

        [Description("სტატისტიკა")]
        Statistics = 8
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}

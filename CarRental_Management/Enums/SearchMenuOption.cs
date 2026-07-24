using System.ComponentModel;


namespace CarRental_Management.Enums
{
    public enum SearchMenuOption
    {
        [Description("მთავარ მენიუში დაბრუნება")]
        Back = 0,

        [Description("ავტომობილის ძებნა ბრენდის მიხედვით")]
        SearchByBrand = 1,

        [Description("ავტომობილის ძებნა მოდელის მიხედვით")]
        SearchByModel = 2,

        [Description("ავტომობილების ფილტრაცია კატეგორიის მიხედვით")]
        FilterByCategory = 3,

        [Description("ავტომობილების ფილტრაცია ფასის მიხედვით")]
        FilterByPrice = 4,

        [Description("ხელმისაწვდომი ავტომობილები")]
        AvailableCars = 5,

        [Description("მომხმარებლის ძებნა პირადი ნომრით")]
        SearchCustomerByPersonalNumber = 6,

        [Description("მომხმარებლის ძებნა ტელეფონის ნომრით")]
        SearchCustomerByPhone = 7
    }
}

using System.ComponentModel;

namespace CarRental_Management.Enums
{
    public enum CustomerMenuOption
    {
        [Description("მთავარ მენიუში დაბრუნება")]
        Back = 0,

        [Description("მომხმარებლების სრული სია")]
        ViewAll = 1,

        [Description("ახალი მომხმარებლის დამატება")]
        Add = 2,

        [Description("მომხმარებლის დეტალების ნახვა")]
        ViewDetails = 3,

        [Description("მომხმარებლის რედაქტირება")]
        Edit = 4,

        [Description("მომხმარებლის წაშლა")]
        Delete = 5,

        [Description("მომხმარებლის გაქირავებების ისტორია")]
        RentalHistory = 6
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CarRental_Management.Enums
{
    public enum CarMenuOption
    {
        [Description("მთავარ მენიუში დაბრუნება")]
        Back = 0,

        [Description("ავტომობილების სრული სია")]
        ViewAll = 1,

        [Description("ახალი ავტომობილის დამატება")]
        Add = 2,

        [Description("ავტომობილის დეტალების ნახვა")]
        ViewDetails = 3,

        [Description("ავტომობილის რედაქტირება")]
        Edit = 4,

        [Description("ავტომობილის წაშლა")]
        Delete = 5,

        [Description("ხელმისაწვდომი ავტომობილების ნახვა")]
        ViewAvailable = 6
    }
}

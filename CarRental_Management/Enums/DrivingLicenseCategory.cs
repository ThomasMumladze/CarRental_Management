using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental_Management.Enums
{
    public enum DrivingLicenseCategory
    {
        None = 0,
        A1   = 1 << 0,   // 1
        A    = 1 << 1,   // 2
        B1   = 1 << 2,   // 4
        B    = 1 << 3,   // 8
        BE   = 1 << 4,   // 16
        C1   = 1 << 5,   // 32
        C    = 1 << 6,   // 64
        C1E  = 1 << 7,   // 128
        CE   = 1 << 8,   // 256
        D1   = 1 << 9,   // 512
        D    = 1 << 10,  // 1024
        D1E  = 1 << 11,  // 2048
        DE   = 1 << 12,  // 4096
        T    = 1 << 13
    }
}

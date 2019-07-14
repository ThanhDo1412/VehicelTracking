using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VT.Data.CommonData
{
    public enum ErrorCode
    {
        [Display(Name = "Internal Server Error")]
        E500,
        [Display(Name = "User not existed!")]
        E001,
        [Display(Name = "Password Incorrect!")]
        E002,
        [Display(Name = "Create User failed. Please try again")]
        E003,
    }
}

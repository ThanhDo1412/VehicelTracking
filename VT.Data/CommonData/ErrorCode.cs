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
        [Display(Name = "User doesn't existed!")]
        E001,
        [Display(Name = "Password Incorrect!")]
        E002,
        [Display(Name = "Create User failed. Please try again")]
        E003,
        [Display(Name = "Vehicle number {0} already registered")]
        E004,
        [Display(Name = "Vehicle number {0} doesn't exist")]
        E005,
        [Display(Name = "Vehicle number {0} never update location")]
        E006,
        [Display(Name = "Can't find journey of vehicle number {0} from {1} to {2}")]
        E007,
    }
}

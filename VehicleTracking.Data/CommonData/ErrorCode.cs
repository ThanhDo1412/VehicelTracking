using System.ComponentModel.DataAnnotations;

namespace VehicleTracking.Data.CommonData
{
    public enum ErrorCode
    {
        [Display(Name = "Internal Server Error")]
        E500,
        //User Error
        [Display(Name = "User doesn't existed!")]
        E001,
        [Display(Name = "Password Incorrect!")]
        E002,
        [Display(Name = "Create user failed. Please try again")]
        E003,
        //User validation error
        [Display(Name = "Email wrong format")]
        E004,
        [Display(Name = "Password has at least 5 characters")]
        E005,
        [Display(Name = "Username has at least 5 characters")]
        E006,
        //Vehicle Error
        [Display(Name = "Vehicle number {0} already registered")]
        E101,
        [Display(Name = "Vehicle number {0} doesn't exist")]
        E102,
        [Display(Name = "Vehicle number {0} never update location")]
        E103,
        [Display(Name = "Can't find journey of vehicle number {0} from {1} to {2}")]
        E104,
        //Vehicle validation Error
        [Display(Name = "Vehicle Number can not be null or empty")]
        E105,
        [Display(Name = "From should be greater than To")]
        E106,
        [Display(Name = "Latitude should be -90 to 90 and Longitude should be -180 to 180")]
        E107
    }
}

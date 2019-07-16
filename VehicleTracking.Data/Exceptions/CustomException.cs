using System;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Helper;

namespace VehicleTracking.Data.Exceptions
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public CustomException(ErrorCode error)
        {
            ErrorCode = error.ToString();
            ErrorMessage = error.GetDisplayAttribute().Name;
        }

        public CustomException(ErrorCode error, params object[] values)
        {
            ErrorCode = error.ToString();
            ErrorMessage = string.Format(error.GetDisplayAttribute().Name, values);
        }
    }
}

using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class PasswordFormatException : CustomException
    {
        public PasswordFormatException(ErrorCode error) : base(error)
        {
        }

        public PasswordFormatException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

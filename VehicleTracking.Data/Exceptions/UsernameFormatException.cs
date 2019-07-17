using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class UsernameFormatException : CustomException
    {
        public UsernameFormatException(ErrorCode error) : base(error)
        {
        }

        public UsernameFormatException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class WrongPasswordException : CustomException
    {
        public WrongPasswordException(ErrorCode error) : base(error)
        {
        }

        public WrongPasswordException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class UserNotFoundException : CustomException
    {
        public UserNotFoundException(ErrorCode error) : base(error)
        {
        }

        public UserNotFoundException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

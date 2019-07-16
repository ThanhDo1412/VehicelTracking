using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class CreateUserFailException : CustomException
    {
        public CreateUserFailException(ErrorCode error) : base(error)
        {
        }

        public CreateUserFailException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

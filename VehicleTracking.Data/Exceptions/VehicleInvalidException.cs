using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class VehicleInvalidException : CustomException
    {
        public VehicleInvalidException(ErrorCode error) : base(error)
        {
        }

        public VehicleInvalidException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

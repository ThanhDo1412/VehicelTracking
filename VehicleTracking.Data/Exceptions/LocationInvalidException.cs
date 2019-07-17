using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class LocationInvalidException : CustomException
    {
        public LocationInvalidException(ErrorCode error) : base(error)
        {
        }

        public LocationInvalidException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

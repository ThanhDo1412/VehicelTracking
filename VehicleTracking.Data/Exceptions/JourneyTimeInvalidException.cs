using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class JourneyTimeInvalidException : CustomException
    {
        public JourneyTimeInvalidException(ErrorCode error) : base(error)
        {
        }

        public JourneyTimeInvalidException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

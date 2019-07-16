using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class LocationNotFoundException : CustomException
    {
        public LocationNotFoundException(ErrorCode error) : base(error)
        {
        }

        public LocationNotFoundException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

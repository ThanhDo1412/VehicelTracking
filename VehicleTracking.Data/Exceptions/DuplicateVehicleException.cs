using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class DuplicateVehicleException : CustomException
    {
        public DuplicateVehicleException(ErrorCode error) : base(error)
        {
        }

        public DuplicateVehicleException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

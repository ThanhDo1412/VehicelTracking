using VehicleTracking.Data.CommonData;

namespace VehicleTracking.Data.Exceptions
{
    public class VehicleNotFoundException : CustomException
    {
        public VehicleNotFoundException(ErrorCode error) : base(error)
        {
        }

        public VehicleNotFoundException(ErrorCode error, params object[] values) : base(error, values)
        {
        }
    }
}

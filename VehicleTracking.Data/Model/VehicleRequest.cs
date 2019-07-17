using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;

namespace VehicleTracking.Data.Model
{
    public class VehicleRequest
    {
        public string VehicleNumber { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(VehicleNumber))
            {
                throw new VehicleInvalidException(ErrorCode.E105);
            }
        }
    }
}

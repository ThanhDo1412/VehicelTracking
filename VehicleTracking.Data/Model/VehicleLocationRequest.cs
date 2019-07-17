using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;

namespace VehicleTracking.Data.Model
{
    public class VehicleLocationRequest
    {
        public string VehicleNumber { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(VehicleNumber))
            {
                throw new VehicleInvalidException(ErrorCode.E105);
            }

            if (Latitude < -90 || Latitude > 90
                || Longitude < -180 || Longitude > 180)
            {
                throw new LocationInvalidException(ErrorCode.E107);
            }
        }
    }
}

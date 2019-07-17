using System;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;

namespace VehicleTracking.Data.Model
{
    public class VehicleJourneyRequest
    {
        public string VehicleNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(VehicleNumber))
            {
                throw new VehicleInvalidException(ErrorCode.E105);
            }

            if (From <= To)
            {
                throw new JourneyNotFoundException(ErrorCode.E106);
            }
        }
    }
}

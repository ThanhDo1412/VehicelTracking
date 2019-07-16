using System;

namespace VehicleTracking.Data.Model
{
    public class VehicleJourneyRequest
    {
        public string VehicleNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}

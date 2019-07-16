using System.Collections.Generic;

namespace VehicleTracking.Data.Model
{
    public class VehicleJourneyResponse
    {
        public string VehicleNumber { get; set; }
        public List<LocationBase> Locations { get; set; }
    }
}

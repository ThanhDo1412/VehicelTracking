using System;

namespace VehicleTracking.Data.Model
{
    public class LocationBase
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime LatestUpdate { get; set; }
    }
}

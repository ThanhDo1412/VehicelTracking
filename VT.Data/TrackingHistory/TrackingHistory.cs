using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.TrackingHistory
{
    public class TrackingHistory
    {
        public Guid VehicleId { get; set; }
        public DateTime TrackedTime { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
    }
}

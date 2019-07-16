using System;

namespace VehicleTracking.Data.TrackingHistory
{
    public class TrackingHistoryBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

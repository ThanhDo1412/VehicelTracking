using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleTracking.Data.TrackingHistory
{
    public class TrackingHistory : TrackingHistoryBase
    {
        public Guid TrackingSessionId { get; set; }
        [Column(TypeName = "DECIMAL(12,9)")]
        public decimal Lat { get; set; }
        [Column(TypeName = "DECIMAL(12,9)")]
        public decimal Lon { get; set; }
        public virtual TrackingSession TrackingSession { get; set; }
    }
}

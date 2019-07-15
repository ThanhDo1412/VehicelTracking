using System;

namespace VT.Data.TrackingHistory
{
    public class TrackingHistory : TrackingHistoryBase
    {
        public Guid TrackingSessionId { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public virtual TrackingSession TrackingSession { get; set; }
    }
}

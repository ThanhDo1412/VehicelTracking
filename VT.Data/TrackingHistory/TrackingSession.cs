using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.TrackingHistory
{
    public class TrackingSession : TrackingHistoryBase
    {
        public string TrackingRemark { get; set; }
        public Guid VehicleId { get; set; }
        public ICollection<TrackingHistory> TrackingHistories { get; set; }
    }
}

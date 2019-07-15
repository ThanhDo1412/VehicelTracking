using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.TrackingHistory
{
    public class TrackingHistoryBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}

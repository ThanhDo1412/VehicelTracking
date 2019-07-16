using Microsoft.EntityFrameworkCore;

namespace VehicleTracking.Data.TrackingHistory
{
    public class TrackingHistoryContext : DbContext
    {
        public TrackingHistoryContext(DbContextOptions<TrackingHistoryContext> options)
            : base(options)
        { }

        public DbSet<TrackingHistory> TrackingHistories { get; set; }
        public DbSet<TrackingSession> TrackingSessions { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace VT.Data.TrackingHistory
{
    public class TrackingHistoryContext : DbContext
    {
        public TrackingHistoryContext(DbContextOptions<TrackingHistoryContext> options)
            : base(options)
        { }

        public DbSet<TrackingHistory> TrackingHistories { get; set; }
    }
}

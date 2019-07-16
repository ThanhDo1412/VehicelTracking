using System;
using Microsoft.AspNetCore.Identity;

namespace VehicleTracking.Data.Vehicle
{
    public class VehicleTrackingUser : IdentityUser<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public VehicleTrackingUser()
        {
            Id = new Guid();
            CreatedDate = DateTime.UtcNow;
        }
    }
}

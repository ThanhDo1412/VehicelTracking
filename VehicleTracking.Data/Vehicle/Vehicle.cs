using System;

namespace VehicleTracking.Data.Vehicle
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string VehicleNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

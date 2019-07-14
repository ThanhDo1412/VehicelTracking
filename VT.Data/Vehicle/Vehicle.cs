using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.Vehicle
{
    public class Vehicle : BaseEntity
    {
        public Guid Id { get; set; }
        public String VehicleNumber { get; set; }
        public decimal CurrentLat { get; set; }
        public decimal CurrentLon { get; set; }
        public DateTime CurrentTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.Model
{
    public class VehicleJourneyResponse
    {
        public string VehicleNumber { get; set; }
        public List<LocationBase> Locations { get; set; }
    }
}

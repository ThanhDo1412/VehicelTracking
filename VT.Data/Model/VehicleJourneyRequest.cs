using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.Model
{
    public class VehicleJourneyRequest
    {
        public string VehicleNumber { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}

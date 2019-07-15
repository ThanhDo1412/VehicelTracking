using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.Model
{
    public class VehicleLocationRequest
    {
        public string VehicleNumber { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VT.Data.Model
{
    public class LocationBase
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime LatestUpdate { get; set; }
    }
}

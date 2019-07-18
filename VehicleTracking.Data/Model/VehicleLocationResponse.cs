namespace VehicleTracking.Data.Model
{
    public class VehicleLocationResponse : LocationBase
    {
        public string VehicleNumber { get; set; }
        public string Address { get; set; }
    }
}

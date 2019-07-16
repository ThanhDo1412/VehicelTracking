using System.Threading.Tasks;
using VehicleTracking.Data.Model;
using VehicleTracking.Data.Vehicle;

namespace VehicleTracking.Service.Interface
{
    public interface IVehicleService
    {
        Task AddVehicle(VehicleRequest model);
        Task RemoveVehicle(string vehicleNumber);
    }
}

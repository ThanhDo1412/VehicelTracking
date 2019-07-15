using System.Threading.Tasks;
using VT.Data.Model;
using VT.Data.Vehicle;

namespace VT.Service.Interface
{
    public interface IVehicleService
    {
        Task AddVehicle(VehicleRequest model);
        Task RemoveVehicel(string vehicleNumber);
        Task<Vehicle> GetVehicleByNumber(string vehicleNumber);
    }
}

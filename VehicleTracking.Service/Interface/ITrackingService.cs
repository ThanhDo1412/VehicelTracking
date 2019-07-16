using System.Threading.Tasks;
using VehicleTracking.Data.Model;

namespace VehicleTracking.Service.Interface
{
    public interface ITrackingService
    {
        Task UpdateLocation(VehicleLocationRequest model);
        Task<VehicleLocationResponse> GetCurrentLocation(string vehicleNumber);
        Task<VehicleJourneyResponse> GetVehicleJourney(VehicleJourneyRequest model);
    }
}
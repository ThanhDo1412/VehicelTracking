using System.Threading.Tasks;
using VT.Data.Model;

namespace VT.Service.Interface
{
    public interface ITrackingService
    {
        Task UpdateLocation(VehicleLocationRequest model);
        Task<VehicleLocationResponse> GetCurrentLocation(string vehicleNumber);
        Task<VehicleJourneyResponse> GetVehicelJourney(VehicleJourneyRequest model);
    }
}
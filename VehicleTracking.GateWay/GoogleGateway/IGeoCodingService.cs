using System.Threading.Tasks;

namespace VehicleTracking.GateWay.GoogleGateway
{
    public interface IGeoCodingService
    {
        Task<string> ReverseGeoCoding(decimal lon, decimal lat);
    }
}
